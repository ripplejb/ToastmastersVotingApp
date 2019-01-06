using System.Collections.Generic;
using Moq;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;
using Voting.Repositories.ElectionRepositories;
using Voting.Repositories.ElectionRepositories.Savers;

namespace ElectionServiceUnitTests.Arrange
{
    public class TemplateCreateElectionSetup
    {
        public void SetupMock(Mock<IElectionSaver> mockElectionRepository,
            Mock<IBallotService> mockBallotService)
        {
            var mockList = new List<Ballot>()
            {
                new Ballot()
                {
                    Id = 1,
                    Name = "Speakers"
                }
            };
            mockElectionRepository.Setup(repo => repo.AddAsync(It.IsAny<Election>()))
                .ReturnsAsync((Election election) => election);
            mockBallotService.Setup(repo => repo.GetDefaultBallotsFromTemplateAsync(It.IsAny<string>()))
                .ReturnsAsync(mockList);
            
        }

    }
}