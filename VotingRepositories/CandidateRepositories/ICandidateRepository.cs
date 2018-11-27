using Voting.ServiceContracts.Models;

namespace VotingRepositories.CandidateRepositories
{
    public interface ICandidateRepository
    {
        Candidate Add(Candidate candidate);
    }
}