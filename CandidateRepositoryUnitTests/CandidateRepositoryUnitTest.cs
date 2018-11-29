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
        public async void CreateTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(GetDbContextOptions()))
            {
                var repository = new CandidateRepository(context);

                // Act
                await repository.AddAsync(new Candidate()
                {
                    Id = 1,
                    Name = "Ripal Barot"
                });
                await repository.AddAsync(new Candidate()
                {
                    Id = 2,
                    Name = "Falguni Barot"
                });
                await repository.AddAsync(new Candidate()
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
        public async void DuplicateTestAsync()
        {
            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                // Arrange
                using (var context = new VotingContext(GetDbContextOptions()))
                {
                    var repository = new CandidateRepository(context);

                    // Act
                    await repository.AddAsync(new Candidate()
                    {
                        Id = 1,
                        Name = "Jashavant Barot"
                    });
                    await repository.AddAsync(new Candidate()
                    {
                        Id = 2,
                        Name = "Jashavant Barot"
                    });
                }
            });
        }

        [Fact]
        public async void UpdateTestAsync()
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
                await repository.AddAsync(candidate);
                candidate.Name = "Anila Barot";
                await repository.UpdateAsync(candidate);

                // Assert
                Assert.Equal("Anila Barot",
                    context.Candidates.Where(c => c.Name == "Anila Barot").Select(c => c.Name).FirstOrDefault());
                
            }
        }
        
        [Fact]
        public async void RemoveTestAsync()
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
                await repository.AddAsync(candidate);
                await repository.RemoveAsync(candidate);

                // Assert
                Assert.Equal(0,
                    context.Candidates.Count());
                
            }
        }
        
        /// <summary>
        /// As long as the Id is same, the remove will work.
        /// </summary>
        [Fact]
        public async void RemoveAfterChangeTestAsync()
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
                await repository.AddAsync(candidate);
                candidate.Name = "Neil Barot";
                await repository.RemoveAsync(candidate);

                // Assert
                Assert.Equal(0,
                    context.Candidates.Count());
                
            }
        }

        [Fact]
        public async void SearchTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(GetDbContextOptions()))
            {
                var repository = new CandidateRepository(context);

                // Act
                await repository.AddAsync(new Candidate()
                {
                    Id = 1,
                    Name = "Ripal Barot"
                });
                await repository.AddAsync(new Candidate()
                {
                    Id = 2,
                    Name = "Falguni Barot"
                });
                await repository.AddAsync(new Candidate()
                {
                    Id = 3,
                    Name = "Neil Barot"
                });

                // Assert
                var candidateList = await repository.SearchAsync(new CandidateSearchRequest
                {
                    Name = "Barot",
                });
                Assert.Equal(3, candidateList.Count());
            }
        }

        [Fact]
        public async void GetByIdTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(GetDbContextOptions()))
            {
                var repository = new CandidateRepository(context);

                // Act
                await repository.AddAsync(new Candidate()
                {
                    Id = 1,
                    Name = "Ripal Barot"
                });
                await repository.AddAsync(new Candidate()
                {
                    Id = 2,
                    Name = "Falguni Barot"
                });
                await repository.AddAsync(new Candidate()
                {
                    Id = 3,
                    Name = "Neil Barot"
                });

                // Assert
                var candidate = await repository.GetByIdAsync(2);
                Assert.Equal("Falguni Barot", candidate.Name);
            }
            
        }

    }
}