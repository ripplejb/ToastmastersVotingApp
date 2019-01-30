using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Voting.ServiceContracts.DbContexts;


namespace UnitTestDbContextOptionProvider
{
    public class SqliteProvider
    {
        public DbContextOptions<VotingContext> GetDbContextOptions()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<VotingContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new VotingContext(options))
            {
                context.Database.EnsureCreated();
            }

            return options;
        }
    }
}