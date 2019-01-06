using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;
using UnitTestDbContextOptionProvider;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;
using Voting.Repositories.ElectionRepositories;
using Voting.Repositories.ElectionRepositories.Savers;
using Xunit;

namespace ElectionRepositoryUnitTests
{
    public class ElectionSaverUnitTest
    {
        #region Private Methods

        private async void SaveCompleteElection(VotingContext context, IElectionSaver saver)
        {
            var candidate1 = new Candidate()
            {
                Name = "Candidate 1"
            };
            var candidate2 = new Candidate()
            {
                Name = "Candidate 1"
            };

            candidate1 = (await context.Candidates.AddAsync(candidate1)).Entity;
            candidate2 = (await context.Candidates.AddAsync(candidate2)).Entity;

            var ballot1 = new Ballot()
            {
                Name = "Speaker 1",
            };
            ballot1 = (await context.Ballots.AddAsync(ballot1)).Entity;
            
            var ballot2 = new Ballot()
            {
                Name = "Speaker 2",
            };
            ballot2 = (await context.Ballots.AddAsync(ballot2)).Entity;

            var bc1 = new BallotCandidate()
            {
                BallotId = ballot1.Id,
                CandidateId = candidate1.Id
            };
            bc1 = (await context.BallotCandidates.AddAsync(bc1)).Entity;
            var bc2 = new BallotCandidate()
            {
                BallotId = ballot1.Id,
                CandidateId = candidate2.Id
            };
            bc2 = (await context.BallotCandidates.AddAsync(bc2)).Entity;
            var bc3 = new BallotCandidate()
            {
                BallotId = ballot2.Id,
                CandidateId = candidate1.Id
            };
            bc3 = (await context.BallotCandidates.AddAsync(bc3)).Entity;
            var bc4 = new BallotCandidate()
            {
                BallotId = ballot2.Id,
                CandidateId = candidate2.Id
            };
            bc4 = (await context.BallotCandidates.AddAsync(bc4)).Entity;

            var election = new Election()
            {
                CreatedDate = DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)),
                ElectionQr = Guid.NewGuid(),
                Ballots = new List<Ballot>()
                {
                    ballot1, ballot2
                },
                ExpirationDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0))
            };
            await saver.AddAsync(election);
        }

        #endregion

        #region Public Methods

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
                
                SaveCompleteElection(context, saver);

                // Act
                await saver.RemoveAllExpiredElectionsAsync();


                // Assert
                Assert.True(context.Elections.Count() == 1);
                
            }
        }
        

        #endregion
        
    }
}