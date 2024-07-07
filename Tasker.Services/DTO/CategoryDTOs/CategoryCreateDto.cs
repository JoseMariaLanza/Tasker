using System.ComponentModel.DataAnnotations;

namespace Tasker.Services.DTO.CategoryDTOs
{
    public class CategoryCreateDto
    {
        [Required]
        [Display(Name = "Category name")]
        public string Name { get; set; }
    }
}
