using System;
using System.ComponentModel.DataAnnotations;

namespace Voting.ServiceContracts.SearchRequests
{
    public class BallotSearchRequest
    {
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}