using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Repositories.BallotRepositories.Builders
{
    public interface IBallotBuilder
    {
        Task<Ballot> GetByIdAsync(int id);
    }
}