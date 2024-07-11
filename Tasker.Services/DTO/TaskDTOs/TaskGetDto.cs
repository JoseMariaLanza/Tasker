using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Repositories.Tasks.Models;
using Tasker.Services.DTO.CategoryDTOs;

namespace Tasker.Services.DTO.TaskDTOs
{
    public class TaskGetDto
    {
        [Required]
        [Display(Name = "Task Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Task name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Task description")]
        public string Description { get; set; }

        public Priorities Priority { get; set; }

        [Display(Name = "Subtasks")]
        public ICollection<TaskGetDto> SubTasks { get; set; }

        [Display(Name = "Associated categories")]
        public ICollection<CategoryGetDto>? Categories { get; set; }
    }
}
