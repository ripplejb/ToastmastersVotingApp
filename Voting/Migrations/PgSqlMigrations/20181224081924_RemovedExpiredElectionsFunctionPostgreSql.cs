using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.PgSqlMigrations
{
    public partial class RemovedExpiredElectionsFunctionPostgreSql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"create function RemoveExpiredElections()
                        returns boolean as $$
                      
                        declare currentDate constant date := now();
                      begin
                        -- Remove all expired elections, ballots and candidates
                      
                          create table temp_CandidatesIds (Id int);
                      
                          insert into temp_CandidatesIds
                          (Id)
                          select ""CandidateId""
                          from ""BallotCandidates"" bc
                            join ""Ballots"" B2 on bc.""BallotId"" = B2.""Id""
                            join ""Elections"" E2 on B2.""ElectionId"" = E2.""Id""
                          where E2.""ExpirationDate"" < currentDate;
                      
                          create table temp_BallotIds (Id int);
                      
                          insert into temp_BallotIds
                          (Id)
                          select ""BallotId""
                          from ""BallotCandidates"" bc
                            join ""Ballots"" B2 on bc.""BallotId"" = B2.""Id""
                            join ""Elections"" E2 on B2.""ElectionId"" = E2.""Id""
                          where E2.""ExpirationDate"" < currentDate;
                      
                      
                          delete from ""BallotCandidates""
                            where ""Id"" =
                                  (select BC.""Id""
                                  from ""BallotCandidates"" BC
                                    join ""Ballots"" B on BC.""BallotId"" = B.""Id""
                                    join ""Elections"" E on B.""ElectionId"" = E.""Id""
                                  where E.""ExpirationDate"" < currentDate);
                      
                          delete from ""Candidates""
                            where ""Id"" = (select ""Id"" from temp_CandidatesIds);
                      
                          delete from ""Ballots""
                            where ""Id"" = (select ""Id"" from temp_BallotIds);
                      
                          delete from ""Elections""
                            where ""ExpirationDate"" < currentDate;
                      
                        return true;
                      
                        exception
                          when others then
                              RAISE INFO 'Error Name:%',SQLERRM;
                              RAISE INFO 'Error State:%', SQLSTATE;
                          return false;
                      end; $$
                      LANGUAGE plpgsql;";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.Sql("drop function if exists RemoveExpiredElections;");
        }
    }
}
