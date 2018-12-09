using System.ComponentModel.DataAnnotations;

namespace Voting.ServiceContracts.Models
{
    public class CandidateSearchRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}