using System;
using System.Collections.Generic;

namespace Voting.ServiceContracts.Models
{
    public class Election
    {
        public int Id { get; set; }
        public Guid ElectionQr { get; set; }
        public IEnumerable<Ballot> Ballots { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}