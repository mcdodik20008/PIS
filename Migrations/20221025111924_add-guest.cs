using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISWF.Migrations
{
    public partial class addguest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogDate",
                table: "log");
            migrationBuilder.Sql("INSERT INTO \"user\" (login, \"password\") VALUES ('guest', 90120)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LogDate",
                table: "log",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
