using System.ComponentModel.DataAnnotations;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Services.DTO.TaskDTOs
{
    public class TaskUpdateDto
    {
        [Required]
        [Display(Name = "Task Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Display(Name = "Task description")]
        public string Description { get; set; }

        [Display(Name = "Task priority")]
        public Priorities Priority { get; set; }

        [Display(Name = "Parent task (Is subtask of another task")]
        public Guid? ParentTaskId { get; set; } = null;

        [Required]
        [Display(Name = "Selected category Ids")]
        public List<Guid>? CategoryIds { get; set; }
    }
}
