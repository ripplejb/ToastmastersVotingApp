using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.ServiceContracts.Models
{
    public class Election : EntityBase
    {
        public Guid ElectionQr { get; set; }
        public IEnumerable<Ballot> Ballots { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}