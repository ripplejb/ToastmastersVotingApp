using System.Threading.Tasks;
using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotCandidateService;
using Xunit;

namespace BallotCandidateServiceUnitTests
{
    public class ServiceUnitTests
    {
        [Fact]
        public async Task AddNewBallotCandidateTest()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<BallotCandidate>>();
            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<BallotCandidate>()))
                .ReturnsAsync((BallotCandidate bc) =>
                {
                    bc.Id = 1;
                    return bc;
                });

            var ballot = new Ballot
            {
                Name = "Speakers"
            };
            
            var candidate = new Candidate
            {
                Name = "Ripal"
            };

            var service = new BallotCandidateService(mockRepo.Object);

            // Act
            var ballotCandidate = await service.AddAsync(ballot, candidate);

            // Assert
            mockRepo.Verify(repo => repo.AddAsync(It.IsAny<BallotCandidate>()), Times.AtMostOnce);
            Assert.NotNull(ballotCandidate);
            Assert.True(ballotCandidate.Ballot.Name == ballot.Name);
            Assert.True(ballotCandidate.Candidate.Name == candidate.Name);
        }

        [Fact]
        public async Task UpdateBallotCandidateService()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<BallotCandidate>>();
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<BallotCandidate>()))
                .ReturnsAsync((BallotCandidate bc) => bc);

            var ballotCandidate = new BallotCandidate
            {
                BallotId = 1,
                CandidateId = 1,
                VoteCount = 20
            };
            
            var service = new BallotCandidateService(mockRepo.Object);
            
            // Act
            await service.UpdateAsync(ballotCandidate);

            // Assert
            mockRepo.Verify(repo => repo.UpdateAsync(ballotCandidate), Times.AtMostOnce);
        }
 
        [Fact]
        public async Task RemoveBallotCandidateService()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<BallotCandidate>>();
            mockRepo.Setup(repo => repo.RemoveAsync(It.IsAny<BallotCandidate>()))
                .Returns(Task.CompletedTask);

            var ballotCandidate = new BallotCandidate();
            
            var service = new BallotCandidateService(mockRepo.Object);
            
            // Act
            await service.RemoveAsync(ballotCandidate);

            // Assert
            mockRepo.Verify(repo => repo.RemoveAsync(ballotCandidate), Times.AtMostOnce);
        }

    }
}