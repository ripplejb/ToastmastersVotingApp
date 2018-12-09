using System;
using System.ComponentModel.DataAnnotations;

namespace Voting.ServiceContracts.Models
{
    public class BallotSearchRequest
    {
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}