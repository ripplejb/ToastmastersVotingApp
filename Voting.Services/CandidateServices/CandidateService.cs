using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;
using Voting.Services.Exceptions;
using Voting.Repositories.CandidateRepositories;
using Voting.Repositories.CandidateRepositories.Builders;
using Voting.Repositories.CandidateRepositories.Savers;

namespace Voting.Services.CandidateServices
{
    public class CandidateService : ICandidateService
    {
        #region Private Member Variables

        private readonly ICandidateBuilder _candidateBuilder;
        private readonly ICandidateSaver _candidateSaver;

        #endregion

        #region Constructors

        public CandidateService(ICandidateBuilder candidateRepository, ICandidateSaver candidateSaver)
        {
            _candidateBuilder = candidateRepository;
            _candidateSaver = candidateSaver;
        }

        #endregion

        #region Public Methods

        public async Task<Candidate> AddAsync(Candidate candidate)
        {
            try
            {
                return await _candidateSaver.AddAsync(candidate);
            }
            catch (Exception e)
            {
                // Log original exception here.
                throw new DbException<Candidate>("Error saving the new Candidate");
            }
        }

        public async Task<Candidate> UpdateAsync(Candidate candidate)
        {
            try
            {
                return await _candidateSaver.UpdateAsync(candidate);
            }
            catch (Exception e)
            {
                // Log original exception here.
                throw new DbException<Candidate>("Error updating the Candidate");
            }
        }

        public async Task<Candidate> RemoveAsync(Candidate candidate)
        {
            try
            {
                return await _candidateSaver.RemoveAsync(candidate);
            }
            catch (Exception e)
            {
                // Log original exception here.
                throw new DbException<Candidate>("Error removing the Candidate");
            }
        }

        public async Task<IEnumerable<Candidate>> SearchAsync(CandidateSearchRequest candidateSearchRequest)
        {
            return await _candidateBuilder.SearchAsync(candidateSearchRequest);
        }

        #endregion

    }
    
}