using Business.Abstracts;
using Business.Adapters.MailService;
using Core.Constants;
using Core.CrossCuttingConcerns.Mail;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Authentication;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
using Entities.DTOs;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserAuthManager : IUserAuthService
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly IJwtHelper _jwtHelper;
        private readonly IMailService _mailService;
        private readonly IUserLoginValidationService _userLoginValidationService;
        private readonly IUserForgotPasswordService _userForgotPasswordService;
        public UserAuthManager(ICustomerService customerService, IUserService userService, IJwtHelper jwtHelper, IMailService mailService, IUserLoginValidationService userLoginAuthenticationService, IUserForgotPasswordService userForgotPasswordService)
        {
            _customerService = customerService;
            _userService = userService;
            _jwtHelper = jwtHelper;
            _mailService = mailService;
            _userLoginValidationService = userLoginAuthenticationService;
            _userForgotPasswordService = userForgotPasswordService;
        }
        public IDataResult<AccessToken> RegisterCustomer(CustomerForRegisterDto customerForRegisterDto)
        {
            if (CheckIfUserExist(customerForRegisterDto.Email).Success)
                return new ErrorDataResult<AccessToken>(Messages.UserAlreadyExist);

            var userAddedResult = _userService.Add(customerForRegisterDto);

            if (!userAddedResult.Success)
                return new ErrorDataResult<AccessToken>(userAddedResult.Message);

            var customerAddedResult = _customerService.Add(customerForRegisterDto, userAddedResult.Data.Id);

            if (!customerAddedResult.Success)
                return new ErrorDataResult<AccessToken>();

            var jwt = CreateAccessToken(userAddedResult.Data);
            return new SuccessDataResult<AccessToken>(jwt, customerAddedResult.Message);
        }
        public async Task<IDataResult<LoginValidationSession>> Login(UserForLoginDto userForLoginDto)
        {
            var selectedUser = CheckIfUserExist(userForLoginDto.Email);

            if (!selectedUser.Success)
                return new ErrorDataResult<LoginValidationSession>(Messages.UserNotFound);

            var comparePasswordResult = PasswordValidation(selectedUser.Data, userForLoginDto.Password);

            if (!comparePasswordResult.Success)
                return new ErrorDataResult<LoginValidationSession>(Messages.UserPasswordNotMatch);

            var loginValidationCode = _userLoginValidationService.CreateValidationCode(selectedUser.Data);

            await _mailService.SendMail(
                userForLoginDto.Email, 
                new MailBody
                {
                    Body = $"Giriş kodunuz : {loginValidationCode.Value}",
                    Subject = "Giriş isteğinizi doğrulamamız gerekiyor"
                });

            return new SuccessDataResult<LoginValidationSession>(new LoginValidationSession
            {
                Expiration = DateTime.Now.AddMinutes(5),
                Guid = loginValidationCode.Guid
            }, Messages.LoginAuthenticationCodeSent);

        }
        public async Task<IResult> ForgotPassword(string email)
        {
            var selectedUser = _userService.GetByEmail(email);

            if (!selectedUser.Success)
                return new ErrorResult();

            var generatedCode = _userForgotPasswordService.CreateAuthenticationCode(selectedUser.Data);

            await _mailService.SendMail(
                email,
                new MailBody
                {
                    Body = $"Şifre sıfırlama kodunuz : {generatedCode.Value}",
                    Subject = "Şifre Sıfırlama"
                });


            return new SuccessDataResult<ForgotPasswordSession>(new ForgotPasswordSession
            {
                Expiration = DateTime.Now.AddMinutes(5),
                Guid = generatedCode.Guid
            }, Messages.ForgotPasswordResetCodeSent);


        }
        public IResult PasswordValidation(User user, string untrustedPassword)
        {
            if (!HashingHelper.ValidateHash(user.PasswordHash, user.PasswordSalt, untrustedPassword))
                return new ErrorResult(Messages.OldPasswordNotVerified);
            return new SuccessResult();
        }
        public IDataResult<User> CheckIfUserExist(string email)
        {
            var selectedEntity = _userService.GetByEmail(email);
            if (selectedEntity.Success)
                return new SuccessDataResult<User>(selectedEntity.Data);
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }
        public AccessToken CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            return _jwtHelper.CreateAccessToken(user, claims);
        }
        public IResult ChangePassword(UserForChangePasswordDto customerForChangePasswordDto)
        {
            var result = _jwtHelper.VerifyJWTSecurityToken(customerForChangePasswordDto.Token);
            if (!result.Success)
                return new ErrorResult(result.Message);

            var selectedUser = _userService.GetByEmail(result.Data.Value);

            if (!PasswordValidation(selectedUser.Data, customerForChangePasswordDto.OldPassword).Success)
                return new ErrorResult(Messages.OldPasswordNotVerified);

            var isPasswordChanged = _userService.UpdatePasswordByEmail(selectedUser.Data.Email, customerForChangePasswordDto.NewPassword);

            if (!isPasswordChanged.Success)
                return new ErrorResult(isPasswordChanged.Message);
            return new SuccessResult(Messages.PasswordModified);
        }

    }
}
