using Moq;
using Voting.ServiceContracts.Models;
using VotingRepositories.ElectionRepositories;

namespace ElectionServiceUnitTests.Arrange
{
    public class RemoveElectionSetup
    {
        public void SetupMock(Mock<IElectionRepository> mockElectionRepository)
        {
            mockElectionRepository.Setup(repo => repo.RemoveAsync(It.IsAny<Election>()))
                .ReturnsAsync((Election election) => election);
        }
    }
}