using System.Collections.Generic;
using Moq;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;

namespace ElectionServiceUnitTests.Arrange
{
    public class TemplateCreateElectionSetup
    {
        public readonly string TEMPLATE = @"{
                                                ""Ballots"":[
                                                    {
                                                        ""Name"": ""Speakers""
                                                    },
                                                    {
                                                        ""Name"": ""Evaluators""
                                                    },
                                                    {
                                                        ""Name"": ""Table Topic Speakers""
                                                    }
                                            
                                                ],
                                                ""ExpirationDays"": 5
                                            }";

        public void SetupMock(Mock<IRepository<Election>> mockElectionRepository,
            Mock<IBallotService> mockBallotService)
        {
            mockElectionRepository.Setup(repo => repo.AddAsync(It.IsAny<Election>()))
                .ReturnsAsync((Election election) => election);
            mockBallotService.Setup(repo => repo.AddAsync(It.IsAny<Ballot>()))
                .ReturnsAsync((Ballot ballot) => ballot);
            
            mockBallotService.Verify(service => service.AddAsync(It.IsAny<Ballot>()), Times.AtLeastOnce);
            
        }

    }
}