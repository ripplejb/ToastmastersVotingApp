using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations
{
    public partial class DatesMovedToElection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_Name",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Ballots");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Ballots");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Elections",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Elections",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Candidates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Elections");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Candidates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Ballots",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Ballots",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Name",
                table: "Candidates",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
