using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace VotingRepositories.ElectionRepositories.Savers
{
    public interface IElectionSaver
    {
        Task<Election> AddAsync(Election election);
        Task<Election> RemoveAsync(Election election);
    }
}