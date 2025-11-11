using System.ComponentModel.DataAnnotations;

namespace SubscriptionAssistant.Models.DTO
{
    public class CreateSubscriptionDTO
    {
        [Required(ErrorMessage = "Название подписки обязательно")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Цена обязательна")]
        [Range(0.01, 100000, ErrorMessage = "Цена должна быть от 0.01 до 100000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Дата начала обязательна")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Цикл оплаты обязателен")]
        [RegularExpression("monthly|yearly", ErrorMessage = "Цикл оплаты должен быть 'monthly' или 'yearly'")]
        public string BillingCycle { get; set; } = "monthly";

        [Required(ErrorMessage = "Категория обязательна")]
        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор категории должен быть положительным числом")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Сервис обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор сервиса должен быть положительным числом")]
        public int ServiceId { get; set; }
    }
}
