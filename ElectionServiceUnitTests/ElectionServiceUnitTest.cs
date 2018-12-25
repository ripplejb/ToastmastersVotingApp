using System;
using System.Collections.Generic;
using System.Linq;
using ElectionServiceUnitTests.Arrange;
using Moq;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;
using Voting.Services.ElectionServices;
using VotingRepositories.ElectionRepositories;
using Xunit;

namespace ElectionServiceUnitTests
{
    public class ElectionServiceUnitTest
    {

        #region Private Methods

        private Mock<IBallotService> GetBallotServiceMock()
        {
            return new Mock<IBallotService>();
        }

        private Mock<IElectionRepository> GetElectionRepositoryMock()
        {
            return new Mock<IElectionRepository>();
        }
        
        #endregion
        
        #region Public Methods

        [Fact]
        public async void CreateDefaultElectionUnitTest()
        {
            
            // Arrange
            var mockBallotService = GetBallotServiceMock();
            var mockElectionRepository = GetElectionRepositoryMock();

            new TemplateCreateElectionSetup().SetupMock(mockElectionRepository, mockBallotService);
            
            var electionService = new ElectionService(mockElectionRepository.Object, mockBallotService.Object);

            // Act
            Election result = await electionService.CreateElectionUsingTemplateAsync("default", new Election()
            {
                ElectionQr = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
            });

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result?.Ballots);
            Assert.True((result?.Ballots).Any());
        }
        
        [Fact]
        public async void CreateCustomElectionUnitTest()
        {
            
            // Arrange
            var mockElectionRepository = GetElectionRepositoryMock();
            var createElectionSetup = new CreateElectionSetup();
            createElectionSetup.SetupMock(mockElectionRepository);
            var election = new Election()
            {
                ElectionQr = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow
            };
            var electionService = new ElectionService(mockElectionRepository.Object, null);
            
            // Act
            Election result = await electionService.AddAsync(election);
            
            // Assert
            Assert.True(result.Id > 0);
            Assert.Null(result.Ballots);
        }

        [Fact]
        public async void RemoveElectionUnitTest()
        {
            // Arrange
            var mockElectionRepository = GetElectionRepositoryMock();
            var deleteElectionSetup = new RemoveElectionSetup();

            var election = new Election()
            {
                Id = 1,
                Ballots = new[] {new Ballot()},
                CreatedDate = DateTime.Now,
                ElectionQr = Guid.NewGuid(),
                ExpirationDate = DateTime.Now.Add(new TimeSpan(7, 0, 0, 0))
            };

            Election repoResult = election;

            deleteElectionSetup.SetupMock(mockElectionRepository, 
                (e) => { 
                repoResult = e;
                return e;
            });
            var electionService = new ElectionService(mockElectionRepository.Object, null);
            
            // Act
            Election result = await electionService.RemoveAsync(election);
            
            // Assert
            Assert.Same(repoResult, result);
            Assert.True(result.Id == 1);
        }
        
        [Fact]
        public void RemoveAllExpiredElectionUnitTest()
        {
            // Arrange
            
            // Act
            
            // Assert
            throw new NotImplementedException();
        }

        #endregion
    }
}