using System;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;
using Voting.Services.BallotServices;
using VotingRepositories.ElectionRepositories;

namespace Voting.Services.ElectionServices
{
    public class ElectionService : IElectionService
    {
        #region Private Member Variables

        private readonly IElectionRepository _electionRepository;
        private readonly IBallotService _ballotService;

        #endregion
        
        #region Constructors

        public ElectionService(IElectionRepository electionRepository, IBallotService ballotService)
        {
            _electionRepository = electionRepository;
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
            election = await _electionRepository.AddAsync(election);
            return election;
        }

        public async Task<Election> RemoveAsync(Election election)
        {
            return await _electionRepository.RemoveAsync(election);
        }

        #endregion
    }
}