using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;

namespace ElectionServiceUnitTests.Arrange
{
    public class CreateElectionSetup
    {
        public void SetupMock(Mock<IRepository<Election>> mockElectionRepository)
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