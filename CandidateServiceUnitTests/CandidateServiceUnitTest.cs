using System.Collections.Generic;
using System.Linq;
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
        
        #region Private Methods

        private List<Candidate> GetCandidateList()
        {
            return new List<Candidate>
            {
                new Candidate()
                {
                    Id = 1,
                    Name = "Ripal"
                },
                new Candidate()
                {
                    Id = 2,
                    Name = "Rizwan"
                },
                new Candidate()
                {
                    Id = 3,
                    Name = "Jason"
                }
            };
        }

        #endregion

        #region Public Methods

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
                .Throws<DbException<Candidate>>();

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
            await Assert.ThrowsAsync<DbException<Candidate>>(async () => await candidateService.UpdateAsync(candidate2));
            Assert.Equal("Neil Barot", candidate.Name);
            Assert.Equal(1, candidate.Id);
        }

        [Fact]
        public async void CandidateRemoveTest()
        {
            // Arrange
            var candidates = GetCandidateList();

            var removedCandidate = new Candidate
            {
                Id = 3,
                Name = "Jason"
            };

            var mockRepo = new Mock<ICandidateRepository>();
            mockRepo.Setup(m => m.RemoveAsync(It.IsAny<Candidate>()))
                .ReturnsAsync((Candidate candidate) =>
                {
                    if (candidates.RemoveAll(e => e.Id == candidate.Id) > 0)
                        return candidate;
                    else
                    {
                        throw new DbException<Candidate>();
                    }
                });
            
            var candidateService = new CandidateService(mockRepo.Object);
            
            // Act
            await candidateService.RemoveAsync(removedCandidate);
            
            // Assert
            Assert.Equal(2, candidates.Count);
            await Assert.ThrowsAsync<DbException<Candidate>>(async () => await candidateService.RemoveAsync(removedCandidate));

        }
        
        [Fact]
        public async void CandidateSearchTest()
        {
            // Arrange
            var candidates = GetCandidateList();

            var searchRequest = new CandidateSearchRequest
            {
                Name = "Jason"
            };

            var mockRepo = new Mock<ICandidateRepository>();
            mockRepo.Setup(m => m.SearchAsync(It.IsAny<CandidateSearchRequest>()))
                .ReturnsAsync((CandidateSearchRequest request) =>
                {
                    return candidates.FindAll(e => e.Name == request.Name);
                });
            
            var candidateService = new CandidateService(mockRepo.Object);
            
            // Act
            var res = await candidateService.SearchAsync(searchRequest);
            
            // Assert
            Assert.Equal(1, res.ToList().Count);

        }
        
        #endregion
     
    }
}