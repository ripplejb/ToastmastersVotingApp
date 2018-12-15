using System;
using Moq;
using Voting.ServiceContracts.Models;
using VotingRepositories.ElectionRepositories;
using Xunit;

namespace ElectionServiceUnitTests
{
    public class ElectionServiceUnitTest
    {

        #region Private Member Variables

        private Mock<IBallotService> _mockBallotService;
        private Mock<IElectionRepository> _mockElectionRepository;

        #endregion
        
        #region Public Methods

        [Fact]
        public async void CreateDefaultToastmastersElectionUnitTest()
        {
            // Notes : A new election will be created.
            //         Service will also call a ballot creator
            //         that will call Ballot service to create ballots
            //         In our case, the service will create three ballots.
            //         The ballot service will take the election service as input.
            
            // Arrange
            _mockElectionRepository.Setup(repo => repo.AddAsync(It.IsAny<Election>()))
                .ReturnsAsync((Election election) => election);
            _mockBallotService.Setup(repo => repo.AddAsync(It.IsAny<Ballot>()))
                .ReturnsAsync((Ballot ballot) => ballot);
            var electionService = new ElectionService(_mockElectionRepository.Object, _mockBallotService.Object);

            // Act

            // Assert
        }
        
        [Fact]
        public void CreateCustomElectionUnitTest()
        {
            // Notes : A new election will be created.
            //         No new ballot will be created.
            //         Custom ballots will be created separately

            
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public void DeleteElectionUnitTest()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public void DeleteAllExpiredElectionUnitTest()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        #endregion
    }
}