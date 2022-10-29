using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class addusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO \"user\" (login, \"password\", \"OrganizationId\") VALUES ('org1', 90120, 1)");
            migrationBuilder.Sql("INSERT INTO \"user\" (login, \"password\", \"OrganizationId\") VALUES ('org2', 90120, 2)");
            migrationBuilder.Sql("INSERT INTO \"user\" (login, \"password\", \"MunicipalityId\") VALUES ('muni1', 90120, 1)");
            migrationBuilder.Sql("INSERT INTO \"user\" (login, \"password\", \"MunicipalityId\") VALUES ('muni2', 90120, 2)");
            migrationBuilder.Sql("INSERT INTO \"user\" (login, \"password\", \"MunicipalityId\") VALUES ('admin', 90120, 4)");
            
            migrationBuilder.Sql("INSERT INTO \"RoleUser\"  VALUES (2, 'muni1')");
            migrationBuilder.Sql("INSERT INTO \"RoleUser\"  VALUES (2, 'muni2')");
            migrationBuilder.Sql("INSERT INTO \"RoleUser\"  VALUES (3, 'org1')");
            migrationBuilder.Sql("INSERT INTO \"RoleUser\"  VALUES (3, 'org2')");
            migrationBuilder.Sql("INSERT INTO \"RoleUser\"  VALUES (1, 'admin')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
