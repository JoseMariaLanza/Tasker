using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Tasker.Repositories.Auth.Models
{
    public class User
    {
        public User()
        {
            UserTypeId = null; // default value
            Emails = new List<UserEmail>();
            IsActive = false;
        }

        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual ICollection<UserEmail> Emails { get; set; }

        public int? UserTypeId { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        public virtual UserType? UserType { get; set; }

        public bool IsActive { get; set; }
    }
}
