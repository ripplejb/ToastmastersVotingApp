using System;
using System.Threading.Tasks;
using Voting.ServiceContracts.Models;
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
            return await _candidateRepository.AddAsync(candidate);
        }

        public async Task<Candidate> UpdateAsync(Candidate candidate)
        {
            return await _candidateRepository.UpdateAsync(candidate);
        }

        #endregion

    }}