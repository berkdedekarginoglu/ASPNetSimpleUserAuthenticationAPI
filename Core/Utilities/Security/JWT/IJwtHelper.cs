using Core.Entities;
using Core.Utilities.Results;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Core.Utilities.Security.JWT
{
    public interface IJwtHelper
    {
        AccessToken CreateAccessToken(User user, List<OperationClaim> operationClaims);

        IDataResult<System.Security.Claims.Claim> VerifyJWTSecurityToken(string token);
    }
}
