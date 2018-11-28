using System;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;
using VotingRepositories.CandidateRepositories;

namespace CandidateRepositoryUnitTests
{
    public class CandidateRepositoryUnitTest
    {
        #region Private Methods

        private DbContextOptions<VotingContext> GetDbContextOptions()
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

        #endregion

        [Fact]
        public void CreateTest()
        {
            // Arrange
            using (var context = new VotingContext(GetDbContextOptions()))
            {
                var repository = new CandidateRepository(context);

                // Act
                repository.Add(new Candidate()
                {
                    Id = 1,
                    Name = "Ripal Barot"
                });
                repository.Add(new Candidate()
                {
                    Id = 2,
                    Name = "Falguni Barot"
                });
                repository.Add(new Candidate()
                {
                    Id = 3,
                    Name = "Neil Barot"
                });

                // Assert

                Assert.Equal(3, context.Candidates.Count());
                Assert.Equal("Neil Barot",
                    context.Candidates.Where(c => c.Name == "Neil Barot").Select(c => c.Name).FirstOrDefault());
            }
        }

        [Fact]
        public void DuplicateTest()
        {
            // Assert
            Assert.Throws<DbUpdateException>(() =>
            {
                // Arrange
                using (var context = new VotingContext(GetDbContextOptions()))
                {
                    var repository = new CandidateRepository(context);

                    // Act
                    repository.Add(new Candidate()
                    {
                        Id = 1,
                        Name = "Jashavant Barot"
                    });
                    repository.Add(new Candidate()
                    {
                        Id = 2,
                        Name = "Jashavant Barot"
                    });
                }
            });
        }

        [Fact]
        public void UpdateTest()
        {
            // Arrange
            using (var context = new VotingContext(GetDbContextOptions()))
            {
                var repository = new CandidateRepository(context);
                var candidate = new Candidate
                {
                    Id = 1,
                    Name = "Ripal Barot"
                };
                
                
                // Act
                repository.Add(candidate);
                candidate.Name = "Anila Barot";
                repository.Update(candidate);
                
                // Assert
                Assert.Equal("Anila Barot",
                    context.Candidates.Where(c => c.Name == "Anila Barot").Select(c => c.Name).FirstOrDefault());
                
            }
        }
    }
}