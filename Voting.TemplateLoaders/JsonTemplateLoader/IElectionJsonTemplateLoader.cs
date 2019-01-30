using Voting.ServiceContracts.Models;

namespace Voting.TemplateLoaders.JsonTemplateLoader
{
    public interface IElectionJsonTemplateLoader
    {
        Election Load(string template);
    }
}