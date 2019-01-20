using System.Collections.Generic;
using Voting.ServiceContracts.Models;

namespace Voting.ServiceContracts.TemplateModels
{
    public class ElectionTemplate
    {
        public IEnumerable<Ballot> Ballots { get; set; }
        public int ExpirationDays { get; set; }
    }
}