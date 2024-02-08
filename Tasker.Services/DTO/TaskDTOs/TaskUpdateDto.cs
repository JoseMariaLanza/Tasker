using System.ComponentModel.DataAnnotations;

namespace Tasker.Services.DTO.TaskDTOs
{
    public class TaskUpdateDto
    {
        [Required]
        [Display(Name = "Task Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Task description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Task priority")]
        public virtual TaskPriorityDto TaskPriority { get; set; }
    }
}
