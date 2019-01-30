using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.ServiceContracts.Models
{
    public class Candidate : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public IEnumerable<BallotCandidate> BallotCandidates { get; set; }
    }
}