using Microsoft.EntityFrameworkCore;

namespace MeadowMewsApi.Models {
    public class LevyStatementContext: DbContext {
        public LevyStatementContext(DbContextOptions<LevyStatementContext> options) 
        : base(options) {}

        public DbSet<LevyStatement> LevyStatements { get; set; }
    }
}