using System.ComponentModel.DataAnnotations.Schema;
using Tasker.Repositories.Auth.Models;
using Tasker.Repositories.Models;

namespace Tasker.Repositories.Tasks.Models
{
    public class TaskItem : BaseModel
    {
        public TaskItem()
        {
            //PriorityId = (int)Priorities.VeryLow;
            //AssignedUser = null;
            //AssignedUserId = null;
            //Categories = new List<TaskItemCategory>();
            //SubTasks = new List<TaskItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? AssignedUserId { get; set; }
        public virtual User? AssignedUser { get; set; }
        public int? PriorityId { get; set; }

        [NotMapped]
        public Priorities? Priority
        {
            get
            {
                return (Priorities?)PriorityId;
            }
            set
            {
                PriorityId = (int?)value;
            }
        }

        public Guid? ParentTaskId { get; set; }

        [ForeignKey(nameof(ParentTaskId))]
        public virtual TaskItem? ParentTask { get; set; }

        public virtual ICollection<TaskItem> SubTasks { get; set; }

        public List<TaskItemCategory>? TaskItemCategories { get; set; } = new();        
    }

    public enum Priorities
    {
        Infinite = 0,
        Urgent = 1,
        VeryHigh = 2,
        High = 3,
        Meddium = 4,
        Low = 5,
        VeryLow = 6,
    }
}
