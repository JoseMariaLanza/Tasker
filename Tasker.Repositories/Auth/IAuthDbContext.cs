using Microsoft.EntityFrameworkCore;
using Tasker.Repositories.Auth.Models;

namespace Tasker.Repositories.Auth
{
    public interface IAuthDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
    }
}
