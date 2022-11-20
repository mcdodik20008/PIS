using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class alter_sequnce_restart_200 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER SEQUENCE ""file_document_id_seq"" RESTART WITH 200;
            ALTER SEQUENCE ""log_id_seq"" RESTART WITH 200;
            ALTER SEQUENCE ""municipality_id_seq"" RESTART WITH 200;
            ALTER SEQUENCE ""organization_id_seq"" RESTART WITH 200;
            ALTER SEQUENCE ""possibility_id_seq"" RESTART WITH 200;
            ALTER SEQUENCE ""register-m-c_id_seq"" RESTART WITH 200;
            ALTER SEQUENCE ""role_id_seq"" RESTART WITH 200;
            ALTER SEQUENCE ""visibility_id_seq"" RESTART WITH 200;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
