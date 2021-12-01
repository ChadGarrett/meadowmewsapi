using Microsoft.EntityFrameworkCore;

namespace MeadowMewsApi.Models
{
    public class HouseholdContext : DbContext
    {
        public HouseholdContext(DbContextOptions<HouseholdContext> options)
            : base(options)
        {
        }

        public DbSet<MeadowMewsApi.Models.Household> Household { get; set; }
    }
}