using System;

namespace Core.Utilities.Security.Authentication
{
    public class LoginValidationSession
    {
        public DateTime Expiration { get; set; }
        public Guid Guid { get; set; }
    }
}
