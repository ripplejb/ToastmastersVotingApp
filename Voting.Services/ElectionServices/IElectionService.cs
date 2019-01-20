using System.Threading.Tasks;
using Voting.ServiceContracts.Models;
using Voting.TemplateLoaders.JsonTemplateLoader;

namespace Voting.Services.ElectionServices
{
    public interface IElectionService
    {
        Task<Election> CreateElectionUsingTemplateAsync(string templatePath, IElectionJsonTemplateLoader templateLoader);
        Task<Election> AddAsync(Election election);
        Task RemoveAsync(Election election);
        Task RemoveAllExpiredElectionsAsync();
    }
}