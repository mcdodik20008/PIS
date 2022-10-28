using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class addvisibilitydata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                " INSERT INTO visibility  (id, \"rate\") VALUES (1, 'Реестра');" +
                " INSERT INTO visibility  (id, \"rate\") VALUES (2, 'Муниципальный'); " +
                " INSERT INTO visibility  (id, \"rate\") VALUES (3, 'Организации'); "
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
