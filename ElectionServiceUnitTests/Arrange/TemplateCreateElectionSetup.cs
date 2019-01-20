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

    }
}