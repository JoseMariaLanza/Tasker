

using System.ComponentModel.DataAnnotations.Schema;
using Tasker.Repositories.Categories.Models;

namespace Tasker.Repositories.Tasks.Models
{
    public class TaskItemCategory
    {
        public Guid TaskItemId { get; set; }

        [NotMapped]
        public virtual TaskItem TaskItem { get; set; }

        public Guid CategoryId { get; set; }

        [NotMapped]
        public virtual Category Category { get; set; }
    }
}
