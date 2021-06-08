using Business.Abstracts;
using Core.Constants;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Authentication;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;

namespace Business.Concretes
{
    public class UserLoginValidationManager : IUserLoginValidationService
    {
        private readonly IUserLoginValidationDal _userLoginValidationDal;
        private readonly IUserService _userService;
        private readonly IJwtHelper _jwtHelper;
        public UserLoginValidationManager(IUserLoginValidationDal userLoginValidationDal,IUserService userService,IJwtHelper jwtHelper)
        {
            _userLoginValidationDal = userLoginValidationDal;
            _userService = userService;
            _jwtHelper = jwtHelper;

        }

        public IDataResult<UserLoginValidation> GetByGuid(Guid guid)
        {
            var selectedEntity = _userLoginValidationDal.Get(e => e.Guid == guid);
            if (selectedEntity == null)
                return new ErrorDataResult<UserLoginValidation>();
            return new SuccessDataResult<UserLoginValidation>(selectedEntity);
        }

        public IResult UpdateSuccessStatusById(int id,bool status = true)
        {
            var selectedEntity = _userLoginValidationDal.Get(e => e.Id == id);
            if (selectedEntity == null)
                return new ErrorResult();

            selectedEntity.IsSuccess = status;

            _userLoginValidationDal.Update(selectedEntity);
            return new SuccessResult();
        }

        public IDataResult<AccessToken> ValidateAuthenticationCode(LoginValidationCode loginValidationCode)
        {
            var selectedAuth = _userLoginValidationDal.Get(e => e.Guid == loginValidationCode.Guid);

            if(selectedAuth==null || selectedAuth.IsSuccess==true)
                return new ErrorDataResult<AccessToken>(Messages.LoginSessionNotValid);

            var validationResult = HashingHelper.ValidateHash(selectedAuth.AuthenticationCodeHash, selectedAuth.AuthenticationCodeSalt, loginValidationCode.Value);

            if (!validationResult)
                return new ErrorDataResult<AccessToken>(Messages.LoginAuthenticationFailed);

            var selectedUserResult = _userService.GetById(selectedAuth.UserId);
            if (!selectedUserResult.Success)
                return new ErrorDataResult<AccessToken>(Messages.UserNotFound);

            var operationClaims = _userService.GetClaims(selectedUserResult.Data);

            var jwt = _jwtHelper.CreateAccessToken(selectedUserResult.Data, operationClaims);

            UpdateSuccessStatusById(selectedAuth.Id);

            return new SuccessDataResult<AccessToken>(jwt,Messages.UserLoggedIn);
        }

        public LoginValidationCode CreateValidationCode(User user)
        {
            var generatedCode = AuthenticationHelper.GenerateLoginAuthenticationCode();
            byte[] generatedHash, generatedSalt;
            HashingHelper.GeneratedHash(out generatedHash, out generatedSalt, generatedCode);

            var generatedGuid = Guid.NewGuid();

            _userLoginValidationDal.Add(new Entities.Concretes.UserLoginValidation()
            {
                AuthenticationCodeHash = generatedHash,
                AuthenticationCodeSalt = generatedSalt,
                Guid = generatedGuid,
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                IsSuccess = false
            });

            return new LoginValidationCode
            {
                Value = generatedCode,
                Guid = generatedGuid
            };
        }
    }
}
