using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.ServiceContracts.SearchRequests;
using Voting.Services.Exceptions;

namespace Voting.Services.CandidateServices
{
    public class CandidateService : ICandidateService
    {
        #region Private Member Variables

        private readonly IRepository<Candidate> _repository;

        #endregion

        #region Constructors

        public CandidateService(IRepository<Candidate> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<Candidate> AddAsync(Candidate candidate)
        {
            try
            {
                return await _repository.AddAsync(candidate);
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
                return await _repository.UpdateAsync(candidate);
            }
            catch (Exception e)
            {
                // Log original exception here.
                throw new DbException<Candidate>("Error updating the Candidate");
            }
        }

        public async Task RemoveAsync(Candidate candidate)
        {
            try
            {
                await _repository.RemoveAsync(candidate);
            }
            catch (Exception e)
            {
                // Log original exception here.
                throw new DbException<Candidate>("Error removing the Candidate");
            }
        }

        public async Task<IEnumerable<Candidate>> SearchAsync(CandidateSearchRequest candidateSearchRequest)
        {
            return await _repository.SearchAsync(candidate =>
                    candidate.Name.Contains(candidateSearchRequest.Name)
                );
        }

        #endregion

    }
    
}