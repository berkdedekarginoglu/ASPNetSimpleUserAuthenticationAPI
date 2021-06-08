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
    public class UserForgotPasswordManager : IUserForgotPasswordService
    {
        private readonly IUserForgotPasswordDal _userForgotPasswordDal;
        private readonly IUserService _userService;
        private readonly IJwtHelper _jwtHelper;
        public UserForgotPasswordManager(IUserForgotPasswordDal userForgotPasswordDal,IUserService userService,IJwtHelper jwtHelper)
        {
            _userForgotPasswordDal = userForgotPasswordDal;
            _userService = userService;
            _jwtHelper = jwtHelper;

        }

        public IDataResult<UserForgotPassword> GetByGuid(Guid guid)
        {
            var selectedEntity = _userForgotPasswordDal.Get(e => e.Guid == guid);
            if (selectedEntity == null)
                return new ErrorDataResult<UserForgotPassword>();
            return new SuccessDataResult<UserForgotPassword>(selectedEntity);
        }

        public IResult UpdateSuccessStatusById(int id, bool status = true)
        {
            var selectedEntity = _userForgotPasswordDal.Get(e => e.Id == id);
            if (selectedEntity == null)
                return new ErrorResult();

            selectedEntity.IsSuccess = status;

            _userForgotPasswordDal.Update(selectedEntity);
            return new SuccessResult();
        }

        public IResult ValidateCode(ForgotPasswordResetCodeClient forgotPasswordResetCodeClient)
        {
            var selectedAuth = _userForgotPasswordDal.Get(e => e.Guid == forgotPasswordResetCodeClient.Guid);

            if (selectedAuth == null || selectedAuth.IsSuccess == true)
                return new ErrorResult(Messages.LoginSessionNotValid);

            var validationResult = HashingHelper.ValidateHash(selectedAuth.ResetCodeHash, selectedAuth.ResetCodeSalt, forgotPasswordResetCodeClient.Code);

            if (!validationResult)
                return new ErrorResult(Messages.LoginAuthenticationFailed);

            _userService.UpdatePasswordById(selectedAuth.UserId, forgotPasswordResetCodeClient.NewPassword);

            UpdateSuccessStatusById(selectedAuth.Id);

            return new SuccessResult(Messages.PasswordModified);

        }

        public ForgotPasswordResetCodeServer CreateAuthenticationCode(User user)
        {
            var generatedCode = AuthenticationHelper.GenerateForgotPasswordResetCode();
            byte[] generatedHash, generatedSalt;
            HashingHelper.GeneratedHash(out generatedHash, out generatedSalt, generatedCode);

            var generatedGuid = Guid.NewGuid();

            _userForgotPasswordDal.Add(new UserForgotPassword()
            {
                ResetCodeHash = generatedHash,
                ResetCodeSalt = generatedSalt,
                Guid = generatedGuid,
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                IsSuccess = false
            });

            return new ForgotPasswordResetCodeServer
            {
                Value = generatedCode,
                Guid = generatedGuid
            };
        }

        public IResult Add(UserForgotPassword userForgotPassword)
        {
            _userForgotPasswordDal.Add(userForgotPassword);
            return new SuccessResult();
            
        }

    }
}
