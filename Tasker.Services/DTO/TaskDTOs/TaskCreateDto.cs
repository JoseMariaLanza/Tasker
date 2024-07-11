using System.ComponentModel.DataAnnotations;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Services.DTO.TaskDTOs
{
    public class TaskCreateDto
    {
        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Display(Name = "Task description")]
        public string Description { get; set; }

        [Display(Name = "Task priority")]
        public Priorities Priority { get; set; }

        [Display(Name = "Parent task (Is subtask of another task")]
        public Guid? ParentTaskId { get; set; }

        [Required]
        [Display(Name = "Categories")]
        public List<Guid>? CategoryIds { get; set; }
    }
}
