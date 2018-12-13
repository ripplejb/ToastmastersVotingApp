using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;
using Voting.Services.Exceptions;
using VotingRepositories.CandidateRepositories;

namespace Voting.Services.CandidateServices
{
    public class CandidateService : ICandidateService
    {
        #region Private Member Variables

        private readonly ICandidateRepository _candidateRepository;

        #endregion

        #region Constructors

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        #endregion

        #region Public Methods

        public async Task<Candidate> AddAsync(Candidate candidate)
        {
            try
            {
                return await _candidateRepository.AddAsync(candidate);
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
                return await _candidateRepository.UpdateAsync(candidate);
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
                return await _candidateRepository.RemoveAsync(candidate);
            }
            catch (Exception e)
            {
                // Log original exception here.
                throw new DbException<Candidate>("Error removing the Candidate");
            }
        }

        public async Task<IEnumerable<Candidate>> SearchAsync(CandidateSearchRequest candidateSearchRequest)
        {
            return await _candidateRepository.SearchAsync(candidateSearchRequest);
        }

        #endregion

    }
    
}