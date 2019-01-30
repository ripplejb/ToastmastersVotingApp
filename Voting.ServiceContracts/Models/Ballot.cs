using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Voting.ServiceContracts.Models
{
    public class Ballot : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Election Election { get; set; }
        public IEnumerable<BallotCandidate> BallotCandidates { get; set; }
    }
}