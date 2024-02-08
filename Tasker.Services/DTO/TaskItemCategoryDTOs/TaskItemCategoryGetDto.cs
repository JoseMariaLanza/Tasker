using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Repositories.Tasks.Models;
using Tasker.Services.DTO.CategoryDTOs;

namespace Tasker.Services.DTO.TaskItemCategoryDTOs
{
    public class TaskItemCategoryGetDto
    {
        //public int TaskItemId { get; set; }
        //public int CategoryId { get; set; }

        [Display(Name = "Associated categories")]
        public CategoryGetDto Category { get; set; }
    }
}
