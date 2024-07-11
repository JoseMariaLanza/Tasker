using System.ComponentModel.DataAnnotations;

namespace Tasker.Services.DTO.CategoryDTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        [Display(Name = "Category Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Category name")]
        public string Name { get; set; }

        [Display(Name = "Parent category (Is subcategory of another category")]
        public Guid? ParentCategoryId { get; set; } = null;
    }
}
