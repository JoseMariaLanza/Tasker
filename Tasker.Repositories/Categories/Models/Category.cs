using System.ComponentModel.DataAnnotations.Schema;
using Tasker.Repositories.Models;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Categories.Models
{
    public class Category : BaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public virtual Category? ParentCategory { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }

        public virtual List<TaskItemCategory>? TaskItemCategories { get; set; }
    }
}
