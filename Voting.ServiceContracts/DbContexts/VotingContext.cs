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

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Election>()
                .HasMany(i => i.Ballots)
                .WithOne(b => b.Election)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ballot>()
                .HasMany(bc => bc.BallotCandidates)
                .WithOne(b => b.Ballot)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        #endregion

    }
}