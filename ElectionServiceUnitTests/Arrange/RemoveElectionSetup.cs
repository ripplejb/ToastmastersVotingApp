using System;
using Moq;
using Voting.ServiceContracts.Models;
using VotingRepositories.ElectionRepositories;
using VotingRepositories.ElectionRepositories.Savers;

namespace ElectionServiceUnitTests.Arrange
{
    public class RemoveElectionSetup
    {
        public void SetupMock(Mock<IElectionSaver> mockElectionRepository, Func<Election,Election> callback)
        {
            mockElectionRepository.Setup(repo => repo.RemoveAsync(It.IsAny<Election>()))
                .ReturnsAsync((Election election) => election).Callback<Election>((election) => callback(election));
        }
    }
}