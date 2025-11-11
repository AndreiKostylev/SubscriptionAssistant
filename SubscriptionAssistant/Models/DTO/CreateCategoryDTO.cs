using System.ComponentModel.DataAnnotations;

namespace SubscriptionAssistant.Models.DTO
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Название категории обязательно")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 50 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание категории обязательно")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Описание должно быть от 1 до 200 символов")]
        public string Description { get; set; } = string.Empty;
    }
}
