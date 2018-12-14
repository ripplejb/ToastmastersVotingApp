using System.Threading.Tasks;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;

namespace VotingRepositories.ElectionRepositories
{
    public class ElectionRepository : IElectionRepository
    {
        #region Private Member Variables

        private readonly VotingContext _votingContext; 
        
        #endregion

        #region Constructors

        public ElectionRepository(VotingContext context)
        {
            _votingContext = context;
        }

        #endregion

        #region Public Methods

        public async Task<Election> AddAsync(Election election)
        {
            var result = _votingContext.Elections.Add(election).Entity;
            await _votingContext.SaveChangesAsync();
            return result;
        }

        public async Task<Election> RemoveAsync(Election election)
        {
            var result = _votingContext.Elections.Remove(election).Entity;
            await _votingContext.SaveChangesAsync();
            return result;
        }

        #endregion
    }
}