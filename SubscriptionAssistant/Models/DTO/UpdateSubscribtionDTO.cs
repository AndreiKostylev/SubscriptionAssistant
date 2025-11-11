using System.ComponentModel.DataAnnotations;

namespace SubscriptionAssistant.Models.DTO
{
    public class UpdateSubscriptionDTO
    {
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 100 символов")]
        public string? Name { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Цена должна быть от 0.01 до 100000")]
        public decimal? Price { get; set; }

        [RegularExpression("monthly|yearly", ErrorMessage = "Цикл оплаты должен быть 'monthly' или 'yearly'")]
        public string? BillingCycle { get; set; }

        public bool? IsActive { get; set; }
    }
}
