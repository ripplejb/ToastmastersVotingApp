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

        public async Task<IEnumerable<BallotCandidate>> SearchAsync(BallotCandidateSearchRequest ballotCandidateSearchRequest)
        {
            // TODO Unit test large query.
            return await _repository.SearchAsync(ballotCandidate =>
               
                    (
                        (ballotCandidateSearchRequest.BallotName == null) ||
                        (ballotCandidateSearchRequest.BallotName != null &&
                        ballotCandidate.Ballot.Name.Contains(ballotCandidateSearchRequest.BallotName))
                    )
                    &&
                    (
                        (ballotCandidateSearchRequest.CandidateName == null) ||
                        (ballotCandidateSearchRequest.CandidateName != null &&
                         ballotCandidate.Candidate.Name.Contains(ballotCandidateSearchRequest.CandidateName))
                    )
                    &&
                    (
                        (ballotCandidateSearchRequest.BallotId == null) ||
                        (ballotCandidateSearchRequest.BallotId != null &&
                         ballotCandidate.BallotId == ballotCandidateSearchRequest.BallotId)
                    )
                    &&
                    (
                        (ballotCandidateSearchRequest.CandidateId == null) ||
                        (ballotCandidateSearchRequest.CandidateId != null &&
                         ballotCandidate.CandidateId == ballotCandidateSearchRequest.CandidateId)
                    )
                    &&
                    (
                        (ballotCandidateSearchRequest.ElectionId == null) ||
                        (ballotCandidateSearchRequest.ElectionId != null &&
                         ballotCandidate.Ballot.Election.Id == ballotCandidateSearchRequest.ElectionId)
                    )
            );
        }

        #endregion
    }
}