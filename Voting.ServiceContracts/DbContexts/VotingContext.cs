using Microsoft.EntityFrameworkCore;
using Voting.ServiceContracts.Models;

namespace Voting.ServiceContracts.DbContexts
{
    public class VotingContext : DbContext
    {
        #region Constructors

        public VotingContext(DbContextOptions<VotingContext> options) : base(options)
        {
            
        }
        #endregion

        #region Public Properties

        public DbSet<Ballot> Ballots { get; set; }
        public DbSet<BallotCandidate> BallotCandidates { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Election> Elections { get; set; }

        #endregion

    }
}