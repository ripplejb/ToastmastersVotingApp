using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;

namespace VotingRepositories.CandidateRepositories.Builders
{
    public class CandidateBuilder : ICandidateBuilder
    {
        #region Private Member Variables

        private readonly VotingContext _votingContext; 
        
        #endregion

        #region Constructors

        public CandidateBuilder(VotingContext context)
        {
            _votingContext = context;
        }

        #endregion
        
        #region Public Methods

        public async Task<IEnumerable<Candidate>> SearchAsync(CandidateSearchRequest candidateSearchRequest)
        {
            return await Task<IEnumerable<Candidate>>.Factory.StartNew(() => 
                from candidate in _votingContext.Candidates
                where candidate.Name.Contains(candidateSearchRequest.Name)
                select candidate);
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await Task<Candidate>.Factory.StartNew(() => _votingContext.Candidates.FirstOrDefault(c => c.Id == id));
        }

        #endregion

    }
}