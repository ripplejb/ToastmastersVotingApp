using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestDbContextOptionProvider;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;
using VotingRepositories.ElectionRepositories;
using VotingRepositories.ElectionRepositories.Savers;
using Xunit;

namespace ElectionRepositoryUnitTests
{
    public class ElectionSaverUnitTest
    {
        
        [Fact]
        public async void CreateTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {
                IElectionSaver saver = new ElectionSaver(context);

                // Act
                await saver.AddAsync(new Election()
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5,0,0,0))
                });
                await saver.AddAsync(new Election()
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5,0,0,0))
                });
                await saver.AddAsync(new Election()
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
                IElectionSaver saver = new ElectionSaver(context);

                // Act
                var election1 = new Election
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5, 0, 0, 0)),
                };
                election1 = await saver.AddAsync(election1);
                await saver.RemoveAsync(election1);

                // Assert
                Assert.Empty(context.Elections);
            }
        }

        [Fact]
        public async void RemoveAllExpiredElectionsTestAsync()
        {
            // Arrange
            using (var context = new VotingContext(new SqliteProvider().GetDbContextOptions()))
            {

                IElectionSaver saver = new ElectionSaver(context);

                await saver.AddAsync(new Election
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(5, 0, 0, 0)),
                });

                await saver.AddAsync(new Election
                {
                    ElectionQr = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.Add(new TimeSpan(-15, 0, 0, 0)),
                    ExpirationDate = DateTime.Now.Add(new TimeSpan(-10, 0, 0, 0)),
                });

                // Act
                await saver.RemoveAllExpiredElectionsAsync();


                // Assert
                Assert.True(context.Elections.Count() == 1);
                
            }
        }
    }
}