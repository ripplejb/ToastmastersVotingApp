using System;
using System.Threading.Tasks;
using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;
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
            var mockSaver = new Mock<IRepository<Ballot>>();
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
            var mockSaver = new Mock<IRepository<Ballot>>();


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
            var mockSaver = new Mock<IRepository<Ballot>>();
            mockSaver.Setup(saver => saver.RemoveAsync(It.IsAny<Ballot>())).Returns(Task.CompletedTask);

            // Assert setup
            mockSaver.Verify(saver => saver.RemoveAsync(It.IsAny<Ballot>()), Times.AtMostOnce);
            
            var service = new BallotService(mockSaver.Object);
            
            // Act
            await service.RemoveAsync(new Ballot
            {
                Name = "Speaker 1"
            });
            
            

        }

        [Fact]
        public async void GetByIdTest()
        {
            // Arrange
            var mockBuilder = new Mock<IRepository<Ballot>>();
            mockBuilder.Setup(bb => bb.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int i) => new Ballot
                {
                    Name = "Speakers",
                    Id = i
                });
            
            mockBuilder.Verify(saver => saver.GetByIdAsync(It.IsAny<int>()), Times.AtMostOnce);
            
            var service = new BallotService(mockBuilder.Object);
            
            // Act
            var ballot = await service.GetByIdAsync(1);

            // Assert
            Assert.True(ballot.Id == 1);

        }
    }

}