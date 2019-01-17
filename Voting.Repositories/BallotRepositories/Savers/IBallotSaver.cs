using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Repositories.BallotRepositories.Savers
{
    public interface IBallotSaver
    {
        Task<Ballot> AddAsync(Ballot ballot);
        Task<Ballot> UpdateAsync(Ballot ballot);
        Task DeleteAsync(int id);
    }
}