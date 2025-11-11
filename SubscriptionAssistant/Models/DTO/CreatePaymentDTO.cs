using System.ComponentModel.DataAnnotations;

namespace SubscriptionAssistant.Models.DTO
{
    public class CreatePaymentDTO
    {
        [Required(ErrorMessage = "Сумма платежа обязательна")]
        [Range(0.01, 100000, ErrorMessage = "Сумма должна быть от 0.01 до 100000")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Дата платежа обязательна")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Подписка обязательна")]
        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор подписки должен быть положительным числом")]
        public int SubscriptionId { get; set; }
    }
}
