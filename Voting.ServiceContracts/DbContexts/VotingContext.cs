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

            modelBuilder.Entity<BallotCandidate>()
                .HasOne(b => b.Ballot)
                .WithMany(b => b.BallotCandidates)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BallotCandidate>()
                .HasKey(bc => new {bc.BallotId, bc.CandidateId});

            modelBuilder.Entity<BallotCandidate>()
                .HasOne(bc => bc.Ballot)
                .WithMany(b => b.BallotCandidates)
                .HasForeignKey(bc => bc.BallotId);

            modelBuilder.Entity<BallotCandidate>()
                .HasOne(bc => bc.Candidate)
                .WithMany(c => c.BallotCandidates)
                .HasForeignKey(bc => bc.CandidateId);
        }

        #endregion

    }
}