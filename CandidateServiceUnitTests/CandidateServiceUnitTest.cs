using Moq;
using Voting.ServiceContracts.Models;
using Voting.Services.CandidateServices;
using VotingRepositories.CandidateRepositories;
using Xunit;

namespace CandidateServiceUnitTests
{
    public class CandidateServiceUnitTest
    {
        [Fact]
        public async void CandidateCreateTest()
        {
            // Arrange
            var mock = new Mock<ICandidateRepository>();
            var candidate = new Candidate()
            {
                Name = "Ripal Barot"
            };

            var localCandidate = candidate;
            mock.Setup(repo => repo.AddAsync(localCandidate)).ReturnsAsync(new Candidate()
            {
                Id = 1,
                Name = "Ripal Barot"
            });

            ICandidateService candidateService = new CandidateService(mock.Object);

            // Act
            candidate = await candidateService.AddAsync(candidate);

            // Assert
            Assert.Equal("Ripal Barot", candidate.Name);
            Assert.Equal(1, candidate.Id);
        }

        [Fact]
        public async void CandidateUpdateTest()
        {
            // Arrange
            var mock = new Mock<ICandidateRepository>();
            var candidate = new Candidate()
            {
                Id = 1,
                Name = "Neil Barot"
            };

            var localCandidate = candidate;
            mock.Setup(repo => repo.UpdateAsync(localCandidate)).ReturnsAsync(new Candidate()
            {
                Id = 1,
                Name = "Neil Barot"
            });

            ICandidateService candidateService = new CandidateService(mock.Object);

            // Act
            candidate = await candidateService.UpdateAsync(candidate);

            // Assert
            Assert.Equal("Neil Barot", candidate.Name);
            Assert.Equal(1, candidate.Id);
        }
    }
}