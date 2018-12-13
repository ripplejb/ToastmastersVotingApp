using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Services.CandidateServices
{
    public interface ICandidateService
    {
        Task<Candidate> AddAsync(Candidate candidate);
        Task<Candidate> UpdateAsync(Candidate candidate);
        Task<Candidate> RemoveAsync(Candidate candidate);
        Task<IEnumerable<Candidate>> SearchAsync(CandidateSearchRequest candidateSearchRequest);
    }
}