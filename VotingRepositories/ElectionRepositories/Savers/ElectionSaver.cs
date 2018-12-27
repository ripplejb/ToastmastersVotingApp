using System.Threading.Tasks;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;

namespace VotingRepositories.ElectionRepositories.Savers
{
    public class ElectionSaver : IElectionSaver
    {
        #region Private Member Variables

        private readonly VotingContext _votingContext; 
        
        #endregion

        #region Constructors

        public ElectionSaver(VotingContext context)
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

        public async Task RemoveAllExpiredElectionsAsync()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}