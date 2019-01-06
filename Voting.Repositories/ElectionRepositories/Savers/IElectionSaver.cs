using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Repositories.ElectionRepositories.Savers
{
    public interface IElectionSaver
    {
        Task<Election> AddAsync(Election election);
        Task<Election> RemoveAsync(Election election);
        Task RemoveAllExpiredElectionsAsync();
    }
}