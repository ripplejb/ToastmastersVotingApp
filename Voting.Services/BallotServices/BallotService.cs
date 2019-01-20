using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Repositories;
using Voting.ServiceContracts.Models;

namespace Voting.Services.BallotServices
{
    public class BallotService: IBallotService
    {
        #region Private Member Variables

        private readonly IRepository<Ballot> _repository;

        #endregion

        #region Constructors

        public BallotService(IRepository<Ballot> repository)
        {
            _repository = repository;
        }

        #endregion
        
        #region Public Methods

        public Task<IEnumerable<Ballot>> GetDefaultBallotsFromTemplateAsync(string templateName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Ballot> AddAsync(Ballot ballot)
        {
            return await _repository.AddAsync(ballot);
        }

        public async Task<Ballot> UpdateAsync(Ballot ballot)
        {
            return await _repository.UpdateAsync(ballot);
        }

        public async Task RemoveAsync(Ballot ballot)
        {
            await _repository.RemoveAsync(ballot);
        }

        public async Task<Ballot> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        #endregion

    }
}