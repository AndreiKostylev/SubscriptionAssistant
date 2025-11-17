using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SubscriptionAssistant.Models
{
    namespace SubscriptionManager.Models
    {
        public class User
        {
            public int Id { get; set; }

            [Required]
            [StringLength(50)]
            public string Username { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string PasswordHash { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; set; }

            public int RoleId { get; set; }
            public Role Role { get; set; } = null!;

            public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        }
    }
}
