using System.Collections.Generic;
using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;

namespace ElectionServiceUnitTests.Arrange
{
    public class TemplateCreateElectionSetup
    {
        public void SetupMock(Mock<IRepository<Election>> mockElectionRepository,
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