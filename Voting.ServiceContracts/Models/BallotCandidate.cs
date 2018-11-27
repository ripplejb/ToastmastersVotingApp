namespace Voting.ServiceContracts.Models
{
    public class BallotCandidate
    {
        public int Id { get; set; }
        public Candidate Candidate { get; set; }
        public Ballot Ballot { get; set; }
        public int VoteCount { get; set; }
    }
}