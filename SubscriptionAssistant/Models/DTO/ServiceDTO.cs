using System.ComponentModel.DataAnnotations;

namespace SubscriptionAssistant.Models.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название сервиса обязательно")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 50 символов")]
        public string Name { get; set; } = string.Empty;

        [Url(ErrorMessage = "Логотип должен быть валидным URL")]
        public string? LogoUrl { get; set; }

        [Required(ErrorMessage = "Базовая цена обязательна")]
        [Range(0, 100000, ErrorMessage = "Базовая цена должна быть от 0 до 100000")]
        public decimal BasePrice { get; set; }
    }
}
