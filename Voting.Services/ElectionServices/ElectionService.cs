using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.Services.Exceptions;
using Voting.TemplateLoaders.JsonTemplateLoader;

namespace Voting.Services.ElectionServices
{
    public class ElectionService : IElectionService
    {
        #region Private Member Variables

        private readonly IRepository<Election> _repository;

        #endregion

        #region Constructors

        public ElectionService(IRepository<Election> repository)
        {
            _repository = repository;
        }

        #endregion
        
        #region Public Methods

        public async Task<Election> CreateElectionUsingTemplateAsync(string templatePath, 
            IElectionJsonTemplateLoader templateLoader)
        {
            if (!File.Exists(templatePath)) throw new TemplateNotFoundException(templatePath);
            var election = await Task<Election>.Factory.StartNew(() =>
            {
                var template = File.ReadAllText(templatePath);
                return templateLoader.Load(template);
            });
            return await _repository.AddAsync(election);
        }

        public async Task<Election> AddAsync(Election election)
        {
            election = await _repository.AddAsync(election);
            return election;
        }

        public async Task RemoveAsync(Election election)
        {
           await _repository.RemoveAsync(election);
        }

        public async Task RemoveAllExpiredElectionsAsync()
        {
            var elections =
                await _repository.SearchAsync(election => 
                    election.ExpirationDate.CompareTo(DateTime.Now) < 0);
            await Task.WhenAll(elections.Select(election => _repository.RemoveAsync(election)));
        }

        #endregion
    }
}