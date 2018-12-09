using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.ServiceContracts.Models
{
    public class BallotCandidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Candidate Candidate { get; set; }
        public Ballot Ballot { get; set; }
        public int VoteCount { get; set; }
    }
}