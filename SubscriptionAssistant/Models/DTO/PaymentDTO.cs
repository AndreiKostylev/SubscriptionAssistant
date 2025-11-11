using System.ComponentModel.DataAnnotations;

namespace SubscriptionAssistant.Models.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Сумма платежа обязательна")]
        [Range(0.01, 100000, ErrorMessage = "Сумма должна быть от 0.01 до 100000")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
        public bool IsSuccessful { get; set; }

        [Required(ErrorMessage = "Подписка обязательна")]
        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор подписки должен быть положительным числом")]
        public int SubscriptionId { get; set; }

        // ссылка на DTO подписки
        public SubscriptionDTO? Subscription { get; set; }
    }

}
