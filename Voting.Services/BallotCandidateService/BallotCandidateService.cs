using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Repositories;
using Voting.ServiceContracts.Models;
using Voting.ServiceContracts.SearchRequests;

namespace Voting.Services.BallotCandidateService
{
    public class BallotCandidateService : IBallotCandidateService
    {
        #region Private Member Variables

        private readonly IRepository<BallotCandidate> _repository;

        #endregion

        #region Constructors

        public BallotCandidateService(IRepository<BallotCandidate> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<BallotCandidate> AddAsync(Ballot ballot, Candidate candidate)
        {
            var ballotCandidate = new BallotCandidate
            {
                Ballot = ballot,
                Candidate = candidate                
            };

            return await _repository.AddAsync(ballotCandidate);
        }

        public async Task RemoveAsync(BallotCandidate ballotCandidate)
        {
            await _repository.RemoveAsync(ballotCandidate);
        }

        public async Task UpdateAsync(BallotCandidate ballotCandidate)
        {
            await _repository.UpdateAsync(ballotCandidate);
        }

        #endregion
    }
}