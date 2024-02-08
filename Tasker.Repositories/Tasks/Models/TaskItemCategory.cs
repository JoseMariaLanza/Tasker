

namespace Tasker.Repositories.Tasks.Models
{
    public class TaskItemCategory
    {
        public int TaskItemId { get; set; }
        //public TaskItem TaskItem { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
