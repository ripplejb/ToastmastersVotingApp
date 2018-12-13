using System.ComponentModel.DataAnnotations;

namespace Voting.ServiceContracts.Models
{
    public class CandidateSearchRequest
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
    }
}