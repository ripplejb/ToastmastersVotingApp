using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations
{
    public partial class ModelUpdateV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "BallotCandidates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BallotId",
                table: "BallotCandidates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_BallotCandidates_BallotId",
                table: "BallotCandidates",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_BallotCandidates_CandidateId",
                table: "BallotCandidates",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_BallotCandidates_Ballots_BallotId",
                table: "BallotCandidates",
                column: "BallotId",
                principalTable: "Ballots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BallotCandidates_Candidates_CandidateId",
                table: "BallotCandidates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BallotCandidates_Ballots_BallotId",
                table: "BallotCandidates");

            migrationBuilder.DropForeignKey(
                name: "FK_BallotCandidates_Candidates_CandidateId",
                table: "BallotCandidates");

            migrationBuilder.DropIndex(
                name: "IX_BallotCandidates_BallotId",
                table: "BallotCandidates");

            migrationBuilder.DropIndex(
                name: "IX_BallotCandidates_CandidateId",
                table: "BallotCandidates");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "BallotCandidates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BallotId",
                table: "BallotCandidates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
