using System.ComponentModel.DataAnnotations;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Services.DTO.TaskDTOs
{
    public class TaskCreatedDto
    {
        [Required]
        [Display(Name = "Task Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Display(Name = "Task description")]
        public string Description { get; set; }

        [Display(Name = "Task priority")]
        public Priorities Priority { get; set; }

        [Display(Name = "Parent task (Is subtask of another task")]
        public int? ParentTaskId { get; set; } = null;

        [Required]
        [Display(Name = "Selected category Ids")]
        public List<int>? CategoryIds { get; set; }
    }
}
