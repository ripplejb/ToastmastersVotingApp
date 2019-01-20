using System;
using Newtonsoft.Json;
using Voting.ServiceContracts.Models;
using Voting.ServiceContracts.TemplateModels;

namespace Voting.TemplateLoaders.JsonTemplateLoader
{
    public class ElectionJsonTemplateLoader: IElectionJsonTemplateLoader
    {
        #region Public Methods

        public Election Load(string template)
        {
            var electionTemplate = JsonConvert.DeserializeObject<ElectionTemplate>(template);
            var election = new Election
            {
                ElectionQr = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.Add(new TimeSpan(electionTemplate.ExpirationDays, 0, 0, 0)),
                Ballots = electionTemplate.Ballots
            };
            return election;
        }

        #endregion

    }
}