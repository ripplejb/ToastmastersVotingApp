using System;
using System.Net.WebSockets;
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
    }
}