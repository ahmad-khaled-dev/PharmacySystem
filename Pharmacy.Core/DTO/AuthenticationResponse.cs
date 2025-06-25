namespace Pharmacy.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? UserName { set; get; } = string.Empty;

        public string? Token { set; get; } = string.Empty;

        public DateTime ExiprationToken { set; get; }

        public string? RefreshToken { set; get; }=string.Empty;

        public DateTime RefreshExpiration { set; get; }

        public string? Role { set; get; }  

    }
}
