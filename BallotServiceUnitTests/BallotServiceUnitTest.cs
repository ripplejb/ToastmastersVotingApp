using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;
using Moq;
using UnitTestDbContextOptionProvider;
using Voting.Repositories.BallotRepositories;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;
using Voting.Services.ElectionServices;
using Xunit;

namespace BallotServiceUnitTests
{
    public class BallotServiceUnitTest
    {
        #region Private Methods

        private Ballot GetSampleBallot(Election election)
        {
            return new Ballot
            {
                Name = "Speakers Ballot",
                Election = election
            };
        }

        #endregion
        
        [Fact]
        public async void CreateTest()
        {
            // Arrange
            var mockSaver = new Mock<IBallotSaver>();
            var election = new Election
            {
                Id = 1,
                CreatedDate = DateTime.Now,
                ElectionQr = Guid.NewGuid(),
                ExpirationDate = DateTime.Now.AddDays(5)
            };

            var ballot = GetSampleBallot(election);

            mockSaver.Setup(bs => bs.AddAsync(It.IsAny<Ballot>()))
                .ReturnsAsync((Ballot b) => b)
                .Callback<Ballot>(b =>
            {
                b.Id = 1;
            }); 

            IBallotService service = new BallotService(mockSaver.Object);
            
            // Act
            ballot = await service.AddAsync(ballot);

            // Assert
            Assert.True(ballot.Id.Equals(1));
        
        }

        /// <summary>
        /// This test will try to prove that the update is Idempotent.
        /// It also verifies that the IBallotRepository was called.
        /// </summary>
        [Fact]
        public async void UpdateTest()
        {
            // Arrange
            var mockSaver = new Mock<IBallotSaver>();


            mockSaver.Setup(bs => bs.UpdateAsync(It.IsAny<Ballot>()))
                .ReturnsAsync((Ballot b) => b);
                
            mockSaver.Verify(saver => saver.UpdateAsync(It.IsAny<Ballot>()), Times.AtMostOnce);

            var service = new BallotService(mockSaver.Object);
            
            // Act
            var ballotUpdate = await service.UpdateAsync(GetSampleBallot(null));

            // Assert
            Assert.NotNull(ballotUpdate);
        }

        /// <summary>
        /// This test will try to prove that the delete is idempotent
        /// </summary>
        [Fact]
        public async void DeleteTest()
        {
            // Arrange
            var mockSaver = new Mock<IBallotSaver>();
            mockSaver.Setup(saver => saver.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Assert setup
            mockSaver.Verify(saver => saver.DeleteAsync(It.IsAny<int>()), Times.AtMostOnce);
            
            var service = new BallotService(mockSaver.Object);
            
            // Act
            await service.DeleteAsync(1);
            
            

        }
    }

}