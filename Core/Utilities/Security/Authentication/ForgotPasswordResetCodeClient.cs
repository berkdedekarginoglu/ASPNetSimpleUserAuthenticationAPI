using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Authentication
{
    public class ForgotPasswordResetCodeClient
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}
