using Microsoft.EntityFrameworkCore;

    public class ElectricityStatementContext : DbContext
    {
        public ElectricityStatementContext (DbContextOptions<ElectricityStatementContext> options)
            : base(options)
        {
        }

        public DbSet<MeadowMewsApi.Models.ElectricityStatement> ElectricityStatement { get; set; }
    }
