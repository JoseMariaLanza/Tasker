using System.ComponentModel.DataAnnotations.Schema;
using Tasker.Repositories.Auth.Models;

namespace Tasker.Repositories.Tasks.Models
{
    public class TaskItem
    {
        public TaskItem()
        {
            PriorityId = (int)Priorities.VeryLow;
            AssignedUser = null;
            AssignedUserId = null;
        }

        public int Id { get; set; }
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

        public int? ParentTaskId { get; set; }

        [ForeignKey(nameof(ParentTaskId))]
        public virtual TaskItem? ParentTask { get; set; }

        public virtual ICollection<TaskItem> SubTasks { get; set; }

        public virtual List<TaskItemCategory> TaskItemCategories { get; set; }
        
    }

    public enum Priorities
    {
        VeryLow = 0,
        Low = 1,
        Meddium = 2,
        High = 3,
        VeryHigh = 4,
        Urgent = 5,
    }
}
