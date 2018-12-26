using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace VotingRepositories.CandidateRepositories.Builders
{
    public interface ICandidateBuilder
    {
        Task<IEnumerable<Candidate>> SearchAsync(CandidateSearchRequest candidateSearchRequest);
        Task<Candidate> GetByIdAsync(int id);
    }
}