using System.Linq;
using UnitTestDbContextOptionProvider;
using Voting.Repositories;
using Xunit;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;

namespace CandidateRepositoryUnitTests
{
    public class CandidateRepositoryUnitTest
    {
        [Fact]
        public async void CreateTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                var saver = new Repository<Candidate>(context);

                // Act
                await saver.AddAsync(new Candidate()
                {
                    Name = "Ripal Barot"
                });
                await saver.AddAsync(new Candidate()
                {
                    Name = "Falguni Barot"
                });
                await saver.AddAsync(new Candidate()
                {
                    Name = "Neil Barot"
                });

                // Assert

                Assert.Equal(3, context.Candidates.Count());
                
                Assert.Equal("Neil Barot",
                    context.Candidates.Where(c => c.Name == "Neil Barot").Select(c => c.Name).FirstOrDefault());
            }
        }

        [Fact]
        public async void DuplicateAllowedTestAsync()
        {
                // Arrange
                using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
                {
                    var repository = new Repository<Candidate>(context);

                    // Act
                    await repository.AddAsync(new Candidate()
                    {
                        Name = "Jashavant Barot"
                    });
                    await repository.AddAsync(new Candidate()
                    {
                        Name = "Jashavant Barot"
                    });
                    
                    // Assert
                    Assert.Equal(2, context.Candidates.Count());
                }
        }

        [Fact]
        public async void UpdateTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                var saver = new Repository<Candidate>(context);
                var candidate = new Candidate
                {
                    Name = "Ripal Barot"
                };
                
                // Act
                candidate = await saver.AddAsync(candidate);
                candidate.Name = "Anila Barot";
                await saver.UpdateAsync(candidate);

                // Assert
                Assert.Equal("Anila Barot",
                    context.Candidates.Where(c => c.Name == "Anila Barot").Select(c => c.Name).FirstOrDefault());
                
            }
        }
        
        [Fact]
        public async void UpdateTestAsync2()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                var saver = new Repository<Candidate>(context);
                var candidate = new Candidate
                {
                    Name = "Ripal Barot"
                };
                
                // Act
                candidate = await saver.AddAsync(candidate);
                candidate = new Candidate()
                {
                    Id = 3,
                    Name = "Anila Barot"
                };

                var exception = await Record.ExceptionAsync(async () => await saver.UpdateAsync(candidate));
                // Assert
                Assert.NotNull(exception);
                
            }
        }

        [Fact]
        public async void RemoveTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                var saver = new Repository<Candidate>(context);
                var candidate = new Candidate
                {
                    Id = 1,
                    Name = "Ripal Barot"
                };
                
                // Act
                await saver.AddAsync(candidate);
                await saver.RemoveAsync(candidate);

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
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                var saver = new Repository<Candidate>(context);
                var candidate = new Candidate
                {
                    Id = 1,
                    Name = "Ripal Barot"
                };
                
                // Act
                await saver.AddAsync(candidate);
                candidate.Name = "Neil Barot";
                await saver.RemoveAsync(candidate);

                // Assert
                Assert.Equal(0,
                    context.Candidates.Count());
                
            }
        }

        [Fact]
        public async void SearchTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                var repository = new Repository<Candidate>(context);

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

                var candidateList = await repository.SearchAsync(candidate => candidate.Name.Contains("Barot"));
                

                // Assert
                Assert.Equal(3, candidateList.Count());
            }
        }

        [Fact]
        public async void GetByIdTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                var repository = new Repository<Candidate>(context);

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