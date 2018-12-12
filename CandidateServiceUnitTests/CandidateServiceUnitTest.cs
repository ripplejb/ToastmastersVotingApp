using System;
using Moq;
using Voting.ServiceContracts.Models;
using Voting.Services.CandidateServices;
using Voting.Services.Exceptions;
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
                Name = "Ripal Barot"
            };
            

            var localCandidate = candidate;
            mock.Setup(repo => repo.AddAsync(localCandidate)).ReturnsAsync(new Candidate()
            {
                Id = 1,
                Name = "Ripal Barot"
            });

            const string updatedName = "Neil Barot";
            mock.Setup(repo => repo.UpdateAsync(It.Is<Candidate>(c => c.Id == 1 && c.Name == updatedName))).ReturnsAsync(new Candidate
            {
                Id = 1,
                Name = updatedName
            });

            //Since we only added Id = 1. Therefor Id <> 1 should fail
            mock.Setup(repo => repo.UpdateAsync(It.Is<Candidate>(c => c.Id != 1)))
                .Throws<RecordNotFoundException<Candidate>>();

            ICandidateService candidateService = new CandidateService(mock.Object);

            // Act
            candidate = await candidateService.AddAsync(candidate);
            candidate.Name = updatedName;
            candidate = await candidateService.UpdateAsync(candidate);

            var candidate2 = new Candidate()
            {
                Id = 2,
                Name = "Ripal"
            };

            // Assert
            await Assert.ThrowsAsync<RecordNotFoundException<Candidate>>(async () => await candidateService.UpdateAsync(candidate2));
            Assert.Equal("Neil Barot", candidate.Name);
            Assert.Equal(1, candidate.Id);
        }
    }
}