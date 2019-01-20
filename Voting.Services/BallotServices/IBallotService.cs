using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Services.BallotServices
{
    public interface IBallotService
    {
        Task<Ballot> AddAsync(Ballot ballot);
        Task<Ballot> UpdateAsync(Ballot ballot);
        Task RemoveAsync(Ballot ballot);
        Task<Ballot> GetByIdAsync(int id);

    }
}