namespace SubscriptionAssistant.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; } = null!;
    }
}
