using System;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;
using VotingRepositories.ElectionRepositories;
using VotingRepositories.ElectionRepositories.Savers;

namespace Voting.Services.ElectionServices
{
    public class ElectionService : IElectionService
    {
        #region Private Member Variables

        private readonly IElectionSaver _electionSaver;
        private readonly IBallotService _ballotService;

        #endregion
        
        #region Constructors

        public ElectionService(IElectionSaver electionSaver, IBallotService ballotService)
        {
            _electionSaver = electionSaver;
            _ballotService = ballotService;
        }

        #endregion
        
        #region Public Methods

        public async Task<Election> CreateElectionUsingTemplateAsync(string templateName, Election election)
        {
            election.Ballots = await _ballotService.GetDefaultBallotsFromTemplateAsync(templateName);
            return await AddAsync(election);
        }

        public async Task<Election> AddAsync(Election election)
        {
            election = await _electionSaver.AddAsync(election);
            return election;
        }

        public async Task<Election> RemoveAsync(Election election)
        {
            return await _electionSaver.RemoveAsync(election);
        }

        #endregion
    }
}