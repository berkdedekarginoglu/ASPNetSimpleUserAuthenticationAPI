namespace Entities.DTOs
{
    public class UserForChangePasswordDto
    {
        public string Token { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
