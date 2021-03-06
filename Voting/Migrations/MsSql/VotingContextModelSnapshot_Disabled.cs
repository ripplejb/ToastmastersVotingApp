﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Voting.ServiceContracts.DbContexts;

namespace Voting.Migrations.MsSql
{
    [DbContext(typeof(VotingContext))]
    partial class VotingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Voting.ServiceContracts.Models.Ballot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ElectionId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ElectionId");

                    b.ToTable("Ballots");
                });

            modelBuilder.Entity("Voting.ServiceContracts.Models.BallotCandidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BallotId");

                    b.Property<int>("CandidateId");

                    b.Property<int>("VoteCount");

                    b.HasKey("Id");

                    b.HasIndex("BallotId");

                    b.HasIndex("CandidateId");

                    b.ToTable("BallotCandidates");
                });

            modelBuilder.Entity("Voting.ServiceContracts.Models.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("Voting.ServiceContracts.Models.Election", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("ElectionQr");

                    b.Property<DateTime>("ExpirationDate");

                    b.HasKey("Id");

                    b.ToTable("Elections");
                });

            modelBuilder.Entity("Voting.ServiceContracts.Models.Ballot", b =>
                {
                    b.HasOne("Voting.ServiceContracts.Models.Election", "Election")
                        .WithMany("Ballots")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Voting.ServiceContracts.Models.BallotCandidate", b =>
                {
                    b.HasOne("Voting.ServiceContracts.Models.Ballot", "Ballot")
                        .WithMany("BallotCandidates")
                        .HasForeignKey("BallotId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Voting.ServiceContracts.Models.Candidate", "Candidate")
                        .WithMany("BallotCandidates")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
