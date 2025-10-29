using SubscriptionAssistant.Models.SubscriptionManager.Models;

namespace SubscriptionAssistant.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public string BillingCycle { get; set; } = "monthly";
        public bool IsActive { get; set; } = true;

      
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int ServiceId { get; set; }

     
        public User User { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
