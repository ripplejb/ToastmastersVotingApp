using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Repositories.BallotRepositories
{
    public interface IBallotSaver
    {
        Task<Ballot> AddAsync(Ballot ballot);
        Task<Ballot> UpdateAsync(Ballot ballot);
    }
}