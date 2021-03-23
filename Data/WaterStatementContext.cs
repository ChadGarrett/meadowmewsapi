using Microsoft.EntityFrameworkCore;

    public class WaterStatementContext : DbContext
    {
        public WaterStatementContext (DbContextOptions<WaterStatementContext> options)
            : base(options)
        {
        }

        public DbSet<MeadowMewsApi.Models.WaterStatement> WaterStatement { get; set; }
    }
