using Core.Constants;
using Core.Entities;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IConfiguration _configuration;
        private TokenOption _tokenOption;
        private DateTime _expiration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOption = _configuration.GetSection("TokenOption").Get<TokenOption>();
         
        }
        public AccessToken CreateAccessToken(User user,List<OperationClaim> operationClaims)
        {
            _expiration = DateTime.Now.AddMinutes(_tokenOption.TokenExpiration);
            var secretKey = _tokenOption.SecretKey;
            var securityKey = SecurityKeyHelper.CreateSecurityKey(secretKey);
            var signInCredentials = SignInCredentialsHelper.CreateSignInCredentials(securityKey);
            var jwt = CreateJWTSecurityToken(user, operationClaims, _tokenOption, signInCredentials);
            var jsonHandler = new JwtSecurityTokenHandler();
            var generatedJwt = jsonHandler.WriteToken(jwt);

            return new AccessToken
            {
                Expiration = _expiration,
                Token = generatedJwt
            };
        }
        public JwtSecurityToken CreateJWTSecurityToken(User user,List<OperationClaim> operationClaims,TokenOption tokenOption,SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
              issuer:tokenOption.Issuer,
              audience:tokenOption.Audience,
              claims:SetClaims(user,operationClaims),
              notBefore:DateTime.Now,
              expires:_expiration,
              signingCredentials:signingCredentials
              );
        }

        public IDataResult<System.Security.Claims.Claim> VerifyJWTSecurityToken(string token)
        {
            var jsonHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            try
            {
                var claims = jsonHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _tokenOption.Issuer,
                    ValidAudience = _tokenOption.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecretKey)
                }, out securityToken);

                return new SuccessDataResult<System.Security.Claims.Claim>(claims.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email));
            }
            catch(SecurityTokenExpiredException)
            {
                return new ErrorDataResult<System.Security.Claims.Claim>(Messages.SecurityTokenExpired);
            }
            catch(Exception)
            {
                return new ErrorDataResult<System.Security.Claims.Claim>(Messages.SecurityTokenIsNotValid);
            }
        }
        public IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims)
        {
            List<Claim> claims = new List<Claim>();
            claims.AddIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims);
            return claims;
        }
    }
}
