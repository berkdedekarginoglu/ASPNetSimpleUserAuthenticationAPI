using Business.Abstracts;
using Core.Utilities.Security.Authentication;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IUserLoginValidationService _userLoginValidationService;
        private readonly IUserForgotPasswordService _userForgotPasswordService;
        public AuthController(IUserAuthService customerAuthService, IUserLoginValidationService userLoginAuthenticationService, IUserForgotPasswordService userForgotPasswordService)
        {
            _userAuthService = customerAuthService;
            _userLoginValidationService = userLoginAuthenticationService;
            _userForgotPasswordService = userForgotPasswordService;

        }

        [HttpPost("register/customer")]
        public IActionResult RegisterCustomer(CustomerForRegisterDto customerForRegisterDto)
        {
            var result = _userAuthService.RegisterCustomer(customerForRegisterDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("loginvalidation")]
        public IActionResult LoginValidation(LoginValidationCode loginAuthenticationCode)
        {
            var result = _userLoginValidationService.ValidateAuthenticationCode(loginAuthenticationCode);
            if (result.Success)
                return Ok(result);
            return Unauthorized(result);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var result = _userAuthService.Login(userForLoginDto);
            if (result.Result.Success)
                return Ok(result.Result);
            return BadRequest(result.Result);
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePassword(UserForChangePasswordDto customerForChangePasswordDto)
        {
            var result = _userAuthService.ChangePassword(customerForChangePasswordDto);
            if (result.Success)
                return Ok(result);
            return Unauthorized(result);

        }

        [HttpPost("forgotpassword")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult ForgotPassword([FromForm] string email)
        {
            var result = _userAuthService.ForgotPassword(email);
            if (result.Result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("resetpassword/")]
        public IActionResult ResetPassword(ForgotPasswordResetCodeClient forgotPasswordResetCodeClient)
        {
            var result = _userForgotPasswordService.ValidateCode(forgotPasswordResetCodeClient);
            if (result.Success)
                return Ok(result);
            return Unauthorized(result);
        }
    }
}
