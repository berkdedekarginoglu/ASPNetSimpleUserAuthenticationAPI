using Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddIdentifier(this ICollection<Claim> claims,string identifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, identifier));
        }

        public static void AddEmail(this ICollection<Claim> claims,string email)
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }

        public static void AddRoles(this ICollection<Claim> claims,List<OperationClaim> operationClaims)
        {
            foreach (var claim in operationClaims)
                claims.Add(new Claim(ClaimTypes.Role, claim.Name));
        }
    }
}
