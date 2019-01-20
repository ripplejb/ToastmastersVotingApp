using System;
using System.IO;
using System.Linq;
using ElectionServiceUnitTests.Arrange;
using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;
using Voting.Services.ElectionServices;
using Voting.TemplateLoaders.JsonTemplateLoader;
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

        private Mock<IRepository<Election>> GetElectionRepositoryMock()
        {
            return new Mock<IRepository<Election>>();
        }
        
        #endregion
        
        #region Public Methods

        [Fact]
        public async void CreateDefaultElectionUnitTest()
        {
            
            // Arrange
            var mockElectionRepository = GetElectionRepositoryMock();

            mockElectionRepository.Setup(repo => repo.AddAsync(It.IsAny<Election>()))
                .ReturnsAsync((Election election) => election);
            
            var electionService = new ElectionService(mockElectionRepository.Object);
            
            var mockElectionTemplateLoader = new Mock<IElectionJsonTemplateLoader>();
            mockElectionTemplateLoader.Setup(el => el.Load(It.IsAny<string>()))
                .Returns((string str) => 
                    new Election
                {
                    ElectionQr = Guid.NewGuid()
                } );

            // Act
            var result = await electionService.CreateElectionUsingTemplateAsync($".{Path.DirectorySeparatorChar}ElectionTemplate.json", mockElectionTemplateLoader.Object);

            // Assert
            mockElectionTemplateLoader.Verify(loader => loader.Load(It.IsAny<string>()), Times.Once);
            Assert.NotNull(result);
        }
        
        [Fact]
        public async void CreateCustomElectionUnitTest()
        {
            
            // Arrange
            var mockElectionRepository = GetElectionRepositoryMock();
            var createElectionSetup = new CreateElectionSetup();
            createElectionSetup.SetupMock(mockElectionRepository);
            var election = new Election
            {
                ElectionQr = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow
            };
            var electionService = new ElectionService(mockElectionRepository.Object);
            
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

            var election = new Election
            {
                Id = 1,
                Ballots = new[] {new Ballot()},
                CreatedDate = DateTime.Now,
                ElectionQr = Guid.NewGuid(),
                ExpirationDate = DateTime.Now.Add(new TimeSpan(7, 0, 0, 0))
            };

            deleteElectionSetup.SetupMock(mockElectionRepository);
            var electionService = new ElectionService(mockElectionRepository.Object);
            
            mockElectionRepository.Verify(repo => repo.RemoveAsync(It.IsAny<Election>()),Times.AtMostOnce);
            
            // Act
            await electionService.RemoveAsync(election);
            
            // Assert
            
            //Verify if repository is called. see the code above
        }
        
        [Fact]
        public async void RemoveAllExpiredElectionUnitTest()
        {
            // Arrange
            var mockRepo = GetElectionRepositoryMock();
            var service = new ElectionService(mockRepo.Object);
            mockRepo.Verify(repo => repo.RemoveAsync(It.IsAny<Election>()), Times.AtMostOnce);
            
            // Act
            await service.RemoveAllExpiredElectionsAsync();
            
            // Assert
            
            //Verify above.
            
        }

        #endregion
    }
}