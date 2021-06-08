using Core.Utilities.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Authentication
{
    public static class AuthenticationHelper
    {
        private static char[] baseCharacters = new char[]
        {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N',
            'O','P','R','S','T','U','V','Y','Z','W','X','Q','1','2',
            '3','4','5','6','7','8','9'
        };

        public static bool CheckLoginAuthenticationCode(byte[] authenticationCodeHash, byte[] authenticationCodeSalt, string autheticationCode)
        {
            var result = HashingHelper.ValidateHash(authenticationCodeHash, authenticationCodeSalt, autheticationCode);

            if (result)
                return true;
            return false;
        }
        public static string GenerateLoginAuthenticationCode()
        {
            string generatedKey = string.Empty;

            for (int x = 0; x < 6; x++)
                generatedKey += baseCharacters[new Random().Next(0, baseCharacters.Length)].ToString();

            return generatedKey;
        }

        public static string GenerateForgotPasswordResetCode()
        {
            return new Random().Next(1000000, 9999999).ToString();
        }
    }
}
