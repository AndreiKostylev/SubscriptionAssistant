namespace SubscriptionAssistant.Models.DTO
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public UserDTO User { get; set; } = null!;
    }
}
