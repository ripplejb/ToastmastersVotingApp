using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.ServiceContracts.Models
{
    public class BallotCandidate : EntityBase
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int BallotId { get; set; }
        public Ballot Ballot { get; set; }
        public int VoteCount { get; set; }
    }
}