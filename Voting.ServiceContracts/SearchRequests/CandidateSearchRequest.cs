using System.ComponentModel.DataAnnotations;

namespace Voting.ServiceContracts.SearchRequests
{
    public class CandidateSearchRequest
    {
        [StringLength(100)]
        public string Name { get; set; }
    }
}