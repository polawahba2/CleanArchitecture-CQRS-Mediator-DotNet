

namespace Application.DTOs
{
    public class UserDTO : BaseUser
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = "";
        public string Role { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}