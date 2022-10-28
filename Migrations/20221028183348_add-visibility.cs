using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PISWF.Migrations
{
    public partial class addvisibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VisibilityId",
                table: "role",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "visibility",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visibility", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_VisibilityId",
                table: "role",
                column: "VisibilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_role_visibility_VisibilityId",
                table: "role",
                column: "VisibilityId",
                principalTable: "visibility",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_visibility_VisibilityId",
                table: "role");

            migrationBuilder.DropTable(
                name: "visibility");

            migrationBuilder.DropIndex(
                name: "IX_role_VisibilityId",
                table: "role");

            migrationBuilder.DropColumn(
                name: "VisibilityId",
                table: "role");
        }
    }
}
