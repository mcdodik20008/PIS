using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class possibilitydata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                " INSERT INTO possibility  (id, \"rate\") VALUES (1, 'Ведения');" +
                " INSERT INTO possibility  (id, \"rate\") VALUES (2, 'Просмотра'); "
            );
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
