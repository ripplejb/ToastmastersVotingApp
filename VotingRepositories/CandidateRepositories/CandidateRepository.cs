using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.ServiceContracts.DbContexts;
using Voting.ServiceContracts.Models;

namespace VotingRepositories.CandidateRepositories
{
    public class CandidateRepository : ICandidateRepository
    {
        #region Private Member Variables

        private readonly VotingContext _votingContext; 
        
        #endregion

        #region Constructors

        public CandidateRepository(VotingContext context)
        {
            _votingContext = context;
        }

        #endregion
        
        #region Public Methods

        public async Task<Candidate> AddAsync(Candidate candidate)
        {
            var result = _votingContext.Candidates.Add(candidate).Entity;
            await _votingContext.SaveChangesAsync();
            return result;
        }

        public async Task<Candidate> UpdateAsync(Candidate candidate)
        {
            var result = _votingContext.Candidates.Update(candidate).Entity;
            await _votingContext.SaveChangesAsync();
            return result;
        }

        public async Task<Candidate> RemoveAsync(Candidate candidate)
        {
            var result = _votingContext.Candidates.Remove(candidate).Entity;
            await _votingContext.SaveChangesAsync();
            return result;
        }

        public IEnumerable<Candidate> Search(CandidateSearchRequest candidateSearchRequest)
        {
            return from candidate in _votingContext.Candidates
                where candidate.Name.Contains(candidateSearchRequest.Name)
                select candidate;
        }

        public Candidate GetById(int id)
        {
            return _votingContext.Candidates.FirstOrDefault(c => c.Id == id);
        }

        #endregion

    }
}