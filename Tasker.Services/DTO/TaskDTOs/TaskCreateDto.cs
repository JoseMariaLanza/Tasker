using System.ComponentModel.DataAnnotations;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Services.DTO.TaskDTOs
{
    public class TaskCreateDto
    {
        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Task description")]
        public string Description { get; set; }

        //[Required]
        [Display(Name = "Task priority")]
        public Priorities ProrityId { get; set; }

        //[Required]
        [Display(Name = "Parent task (Is subtask of another task")]
        public int? ParentTaskId { get; set; }

        [Required]
        [Display(Name = "Categories")]
        public List<int>? Categories { get; set; }
    }
}
