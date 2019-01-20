using System;
using System.Threading.Tasks;
using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;

namespace ElectionServiceUnitTests.Arrange
{
    public class RemoveElectionSetup
    {
        public void SetupMock(Mock<IRepository<Election>> mockElectionRepository)
        {
            mockElectionRepository.Setup(repo => repo.RemoveAsync(It.IsAny<Election>()))
                .Returns(Task.CompletedTask);
        }
    }
}