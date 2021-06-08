using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Authentication;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserAuthService
    {
        IDataResult<AccessToken> RegisterCustomer(CustomerForRegisterDto customerForRegisterDto);
        Task<IDataResult<LoginValidationSession>> Login(UserForLoginDto userForLoginDto);
        IDataResult<User> CheckIfUserExist(string email);
        IResult ChangePassword(UserForChangePasswordDto customerForChangePasswordDto);
        AccessToken CreateAccessToken(User user);
        Task<IResult> ForgotPassword(string email);
    }
}
