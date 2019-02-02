using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using ElectionServiceUnitTests.Arrange;
using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.ServiceContracts.SearchRequests;
using Voting.Services.BallotServices;
using Voting.Services.ElectionServices;
using Voting.Services.Exceptions;
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
            string templatePath = $".{Path.DirectorySeparatorChar}ElectionTemplate.json";
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
                });


            // Act
            var result = await electionService.CreateElectionUsingTemplateAsync(templatePath, mockElectionTemplateLoader.Object);

            // Assert
            mockElectionTemplateLoader.Verify(loader => loader.Load(It.IsAny<string>()), Times.AtMostOnce);
            
            Assert.NotNull(result);
            await Assert.ThrowsAsync<TemplateNotFoundException>(async () => await electionService.CreateElectionUsingTemplateAsync("test", mockElectionTemplateLoader.Object));
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
            
            // Act
            await service.RemoveAllExpiredElectionsAsync();
            
            // Assert
            mockRepo.Verify(repo => repo.RemoveAsync(It.IsAny<Election>()), Times.AtMostOnce);
            
        }

        [Fact]
        public async void SearchElectionUnitTest()
        {
            // Arrange
            var mockRepo = GetElectionRepositoryMock();

            var service = new ElectionService(mockRepo.Object);

            // Act
            await service.SearchAsync(new ElectionSearchRequest());

            // Assert
            mockRepo.Verify(repo => repo.SearchAsync(It.IsAny<Expression<Func<Election, bool>>>()), Times.AtMostOnce);
        }

        #endregion
    }
}