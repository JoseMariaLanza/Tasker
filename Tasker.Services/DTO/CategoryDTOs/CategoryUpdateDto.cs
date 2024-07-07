using System.ComponentModel.DataAnnotations;

namespace Tasker.Services.DTO.CategoryDTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        [Display(Name = "Category Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category name")]
        public string Name { get; set; }

        [Display(Name = "Parent category (Is subcategory of another category")]
        public int? ParentCategoryId { get; set; } = null;
    }
}
