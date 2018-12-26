using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Services.BallotServices
{
    public interface IBallotService
    {
        Task<IEnumerable<Ballot>> GetDefaultBallotsFromTemplateAsync(string templateName);
    }
}