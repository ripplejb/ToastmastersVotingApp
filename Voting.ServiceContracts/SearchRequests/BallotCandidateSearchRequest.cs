namespace Voting.ServiceContracts.SearchRequests
{
    public class BallotCandidateSearchRequest
    {
        public int BallotId { get; set; }
        public int CandidateId { get; set; }
        public int ElectionId { get; set; }
        public string BallotName { get; set; }
        public string CandidateName { get; set; }
    }
}