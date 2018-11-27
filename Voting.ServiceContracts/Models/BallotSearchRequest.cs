using System;

namespace Voting.ServiceContracts.Models
{
    public class BallotSearchRequest
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}