using System;

namespace Voting.ServiceContracts.Models
{
    public class Ballot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}