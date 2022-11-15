using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class registerMcAddons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "action_date",
                table: "register-m-c",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "register-m-c",
                type: "text",
                nullable: false,
                defaultValue: "test");

            migrationBuilder.AddColumn<string>(
                name: "omsu",
                table: "register-m-c",
                type: "text",
                nullable: false,
                defaultValue: "test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "action_date",
                table: "register-m-c");

            migrationBuilder.DropColumn(
                name: "location",
                table: "register-m-c");

            migrationBuilder.DropColumn(
                name: "omsu",
                table: "register-m-c");
        }
    }
}
