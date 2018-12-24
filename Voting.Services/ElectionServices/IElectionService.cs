using System.Threading.Tasks;
using Voting.ServiceContracts.Models;

namespace Voting.Services.ElectionServices
{
    public interface IElectionService
    {
        Task<Election> CreateElectionUsingTemplateAsync(string templateName, Election election);
        Task<Election> AddAsync(Election election);
        Task<Election> RemoveAsync(Election election);
    }
}