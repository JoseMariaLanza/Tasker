

namespace Tasker.Repositories.Tasks.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TaskItemCategory> TaskItemCategories { get; set; }
    }
}
