namespace Core.Utilities.Security.JWT
{
    public class TokenOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int TokenExpiration { get; set; }
    }
}
