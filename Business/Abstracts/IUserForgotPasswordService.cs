using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Authentication;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
using System;

namespace Business.Abstracts
{
    public interface IUserForgotPasswordService
    {
        public IResult Add(UserForgotPassword userForgotPassword);
        public IDataResult<UserForgotPassword> GetByGuid(Guid guid);

        public IResult UpdateSuccessStatusById(int id, bool status = true);

        public IResult ValidateCode(ForgotPasswordResetCodeClient forgotPasswordResetCodeClient);

        public ForgotPasswordResetCodeServer CreateAuthenticationCode(User user);
    }
}
