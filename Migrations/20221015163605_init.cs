using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PISWF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "municipality",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "register-m-c",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    valid_date = table.Column<DateTime>(type: "Date", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    MunicipalityId = table.Column<long>(type: "bigint", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    subvention_share = table.Column<double>(type: "double precision", nullable: false),
                    amount_money = table.Column<double>(type: "double precision", nullable: false),
                    share_funds_subvention = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_register-m-c", x => x.id);
                    table.ForeignKey(
                        name: "FK_register-m-c_municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "municipality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_register-m-c_organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<long>(type: "bigint", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    MunicipalityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.login);
                    table.ForeignKey(
                        name: "FK_user_municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "municipality",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "organization",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "file_document",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: false),
                    RegisterMCId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_document", x => x.id);
                    table.ForeignKey(
                        name: "FK_file_document_register-m-c_RegisterMCId",
                        column: x => x.RegisterMCId,
                        principalTable: "register-m-c",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    method_name = table.Column<string>(type: "text", nullable: false),
                    log_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorLogin = table.Column<string>(type: "text", nullable: false),
                    json_entity = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_log_user_AuthorLogin",
                        column: x => x.AuthorLogin,
                        principalTable: "user",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<long>(type: "bigint", nullable: false),
                    UsersLogin = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersLogin });
                    table.ForeignKey(
                        name: "FK_RoleUser_role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_user_UsersLogin",
                        column: x => x.UsersLogin,
                        principalTable: "user",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_file_document_RegisterMCId",
                table: "file_document",
                column: "RegisterMCId");

            migrationBuilder.CreateIndex(
                name: "IX_log_AuthorLogin",
                table: "log",
                column: "AuthorLogin");

            migrationBuilder.CreateIndex(
                name: "IX_register-m-c_MunicipalityId",
                table: "register-m-c",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_register-m-c_OrganizationId",
                table: "register-m-c",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersLogin",
                table: "RoleUser",
                column: "UsersLogin");

            migrationBuilder.CreateIndex(
                name: "IX_user_MunicipalityId",
                table: "user",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_user_OrganizationId",
                table: "user",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_document");

            migrationBuilder.DropTable(
                name: "log");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "register-m-c");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "municipality");

            migrationBuilder.DropTable(
                name: "organization");
        }
    }
}
