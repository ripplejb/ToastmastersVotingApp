using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace VotingRepositories.CandidateRepositories.Savers
{
    public interface ICandidateSaver
    {
        Task<Candidate> AddAsync(Candidate candidate);
        Task<Candidate> UpdateAsync(Candidate candidate);
        Task<Candidate>  RemoveAsync(Candidate candidate);
    }
}