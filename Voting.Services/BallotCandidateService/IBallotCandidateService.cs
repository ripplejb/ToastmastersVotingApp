using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;
using Voting.ServiceContracts.SearchRequests;

namespace Voting.Services.BallotCandidateService
{
    public interface IBallotCandidateService
    {
        Task<BallotCandidate> AddAsync(Ballot ballot, Candidate candidate);
        Task RemoveAsync(BallotCandidate ballotCandidate);
        Task UpdateAsync(BallotCandidate ballotCandidate);
        Task<IEnumerable<BallotCandidate>> SearchAsync(BallotCandidateSearchRequest ballotCandidateSearchRequest);
    }
}