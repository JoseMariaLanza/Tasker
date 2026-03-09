using System.ComponentModel.DataAnnotations;
using Tasker.Services.DTO.CategoryDTOs;

namespace Tasker.Services.DTO.TaskItemCategoryDTOs
{
    public class TaskItemCategoryGetDto
    {
        public Guid TaskItemId { get; set; }
        public Guid CategoryId { get; set; }

        [Display(Name = "Associated categories")]
        public CategoryGetDto Category { get; set; }
    }
}
