using Microsoft.EntityFrameworkCore;

namespace MeadowMewsApi.Models
{
public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<MeadowMewsApi.Models.User> User { get; set; }
    }
}