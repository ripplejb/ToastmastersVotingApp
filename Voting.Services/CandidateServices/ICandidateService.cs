using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Services.CandidateServices
{
    public interface ICandidateService
    {
        Task<Candidate> AddAsync(Candidate candidate);
        Task<Candidate> UpdateAsync(Candidate candidate);
    }
}