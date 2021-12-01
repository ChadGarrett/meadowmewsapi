using Microsoft.EntityFrameworkCore;

namespace MeadowMewsApi.Models
{
    public class WaterStatementContext : DbContext
    {
        public WaterStatementContext(DbContextOptions<WaterStatementContext> options)
            : base(options)
        {
        }

        public DbSet<MeadowMewsApi.Models.WaterStatement> WaterStatement { get; set; }
    }
}