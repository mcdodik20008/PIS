using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class possibilityroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
             @"   UPDATE ""role""  SET ""PossibilityId""  = '1' WHERE id = 1;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 2;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 3;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 4;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 5;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 6;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 7;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 8;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 9;
            UPDATE ""role""  SET ""PossibilityId""  = '2' WHERE id = 10;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
