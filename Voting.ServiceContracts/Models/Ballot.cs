using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.ServiceContracts.Models
{
    public class Ballot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Election Election { get; set; }
        public IEnumerable<BallotCandidate> BallotCandidates { get; set; }
    }
}