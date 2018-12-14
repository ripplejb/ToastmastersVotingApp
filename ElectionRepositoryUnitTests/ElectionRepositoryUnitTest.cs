using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestDbContextOptionProvider;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;
using VotingRepositories.ElectionRepositories;
using Xunit;

namespace ElectionRepositoryUnitTests
{
    public class ElectionRepositoryUnitTest
    {
        
        [Fact]
        public async void CreateTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                IElectionRepository repository = new ElectionRepository(context);

                // Act
                await repository.AddAsync(new Election()
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5,0,0,0))
                });
                await repository.AddAsync(new Election()
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5,0,0,0))
                });
                await repository.AddAsync(new Election()
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5,0,0,0))
                });

                // Assert

                Assert.Equal(3, context.Elections.Count());
            }
        }
        
        
        [Fact]
        public async void RemoveTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                IElectionRepository repository = new ElectionRepository(context);

                // Act
                var election1 = new Election
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5, 0, 0, 0)),
                };
                election1 = await repository.AddAsync(election1);

                // Assert
                await repository.RemoveAsync(election1);
                Assert.Empty(context.Elections);
            }
        }
    }
}