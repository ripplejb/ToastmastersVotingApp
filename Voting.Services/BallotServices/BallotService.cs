using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Repositories.BallotRepositories;
using Voting.ServiceContracts.Models;

namespace Voting.Services.BallotServices
{
    public class BallotService: IBallotService
    {
        #region Private Member Variables

        private readonly IBallotSaver _ballotSaver;

        #endregion

        #region Constructors

        public BallotService(IBallotSaver ballotSaver)
        {
            _ballotSaver = ballotSaver;
        }

        #endregion
        
        #region Public Methods

        public Task<IEnumerable<Ballot>> GetDefaultBallotsFromTemplateAsync(string templateName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Ballot> AddAsync(Ballot ballot)
        {
            return await _ballotSaver.AddAsync(ballot);
        }

        public async Task<Ballot> UpdateAsync(Ballot ballot)
        {
            return await _ballotSaver.UpdateAsync(ballot);
        }

        #endregion

    }
}