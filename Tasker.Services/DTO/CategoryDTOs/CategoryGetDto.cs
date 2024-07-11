using System.ComponentModel.DataAnnotations;
using Tasker.Services.DTO.TaskDTOs;

namespace Tasker.Services.DTO.CategoryDTOs
{
    public class CategoryGetDto
    {
        [Required]
        [Display(Name = "Category Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Category name")]
        public string Name { get; set; }

        [Display(Name = "Subcategories")]
        public ICollection<CategoryGetDto> SubCategories { get; set; }
    }
}
