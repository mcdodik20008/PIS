using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class possibilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PossibilityId",
                table: "role",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_PossibilityId",
                table: "role",
                column: "PossibilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_role_visibility_PossibilityId",
                table: "role",
                column: "PossibilityId",
                principalTable: "visibility",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_visibility_PossibilityId",
                table: "role");

            migrationBuilder.DropIndex(
                name: "IX_role_PossibilityId",
                table: "role");

            migrationBuilder.DropColumn(
                name: "PossibilityId",
                table: "role");
        }
    }
}
