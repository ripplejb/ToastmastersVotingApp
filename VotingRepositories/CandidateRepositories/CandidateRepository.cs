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

        public Candidate Add(Candidate candidate)
        {
            var result = _votingContext.Candidates.Add(candidate).Entity;
            _votingContext.SaveChanges();
            return result;
        }

        #endregion

    }
}