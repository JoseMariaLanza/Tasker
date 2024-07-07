

using System.ComponentModel.DataAnnotations.Schema;
using Tasker.Repositories.Categories.Models;

namespace Tasker.Repositories.Tasks.Models
{
    public class TaskItemCategory
    {
        public int TaskItemId { get; set; }

        [NotMapped]
        public virtual TaskItem TaskItem { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]
        public virtual Category Category { get; set; }
    }
}
