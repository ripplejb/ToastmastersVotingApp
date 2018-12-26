using Moq;
using Voting.ServiceContracts.Models;
using VotingRepositories.ElectionRepositories;
using VotingRepositories.ElectionRepositories.Savers;

namespace ElectionServiceUnitTests.Arrange
{
    public class CreateElectionSetup
    {
        public void SetupMock(Mock<IElectionSaver> mockElectionRepository)
        {
            mockElectionRepository.Setup(repo => repo.AddAsync(It.IsAny<Election>()))
                .ReturnsAsync((Election election) =>
                {
                    election.Id = 1;
                    return election;
                });
        }
    }
}