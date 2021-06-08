using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Authentication;
using Core.Utilities.Security.JWT;

namespace Business.Abstracts
{
    public interface IUserLoginValidationService
    {
        LoginValidationCode CreateValidationCode(User user);

        IDataResult<AccessToken> ValidateAuthenticationCode(LoginValidationCode loginAuthentcationCode);

    }
}
