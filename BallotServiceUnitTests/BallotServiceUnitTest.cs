using System;
using System.Collections.Generic;
using System.Linq;
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
            
            var ballot = new Ballot
            {
                Name = "Speakers Ballot",
                Election = election
            };

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
        /// This test will not do much. All it will try to prove that the update is Idempotent.
        /// It also verifies that the IBallotRepository was called.
        /// </summary>
        [Fact]
        public async void UpdateTest()
        {
            // Arrange
            var mockSaver = new Mock<IBallotSaver>();

            var ballot = new Ballot
            {
                Name = "Speakers"
            };

            mockSaver.Setup(bs => bs.UpdateAsync(It.IsAny<Ballot>()))
                .ReturnsAsync((Ballot b) => b);
                
            mockSaver.Verify(saver => saver.UpdateAsync(It.IsAny<Ballot>()), Times.AtMostOnce);

            var service = new BallotService(mockSaver.Object);
            
            // Act
            var ballotUpdate = await service.UpdateAsync(ballot);

            // Assert
            Assert.NotNull(ballotUpdate);
        }
    }

}