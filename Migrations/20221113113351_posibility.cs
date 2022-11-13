using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PISWF.Migrations
{
    public partial class posibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_visibility_PossibilityId",
                table: "role");

            migrationBuilder.CreateTable(
                name: "possibility",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_possibility", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_role_possibility_PossibilityId",
                table: "role",
                column: "PossibilityId",
                principalTable: "possibility",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_possibility_PossibilityId",
                table: "role");

            migrationBuilder.DropTable(
                name: "possibility");

            migrationBuilder.AddForeignKey(
                name: "FK_role_visibility_PossibilityId",
                table: "role",
                column: "PossibilityId",
                principalTable: "visibility",
                principalColumn: "id");
        }
    }
}
