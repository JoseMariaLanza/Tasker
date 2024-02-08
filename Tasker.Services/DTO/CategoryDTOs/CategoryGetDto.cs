using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Services.DTO.CategoryDTOs
{
    public class CategoryGetDto
    {
        [Required]
        [Display(Name = "Category Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category name")]
        public string Name { get; set; }

        //[Required]
        //[Display(Name = "Task item categories")]
        //public ICollection<TaskItemCategory> Categories { get; set; }
    }
}
