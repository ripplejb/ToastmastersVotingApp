using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Repositories.BallotRepositories;
using Voting.Repositories.BallotRepositories.Builders;
using Voting.Repositories.BallotRepositories.Savers;
using Voting.ServiceContracts.Models;

namespace Voting.Services.BallotServices
{
    public class BallotService: IBallotService
    {
        #region Private Member Variables

        private readonly IBallotSaver _ballotSaver;
        private readonly IBallotBuilder _ballotBuilder;

        #endregion

        #region Constructors

        public BallotService(IBallotSaver ballotSaver, IBallotBuilder ballotBuilder)
        {
            _ballotSaver = ballotSaver;
            _ballotBuilder = ballotBuilder;
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

        public async Task DeleteAsync(int id)
        {
            await _ballotSaver.DeleteAsync(id);
        }

        public async Task<Ballot> GetByIdAsync(int id)
        {
            return await _ballotBuilder.GetByIdAsync(id);
        }
        #endregion

    }
}