using Microsoft.EntityFrameworkCore.Migrations;
using PISWF.infrasrtucture.auth.model.entity;

#nullable disable

namespace PISWF.Migrations
{
    public partial class adddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                " INSERT INTO organization  (id, \"name\") VALUES (1, 'Рога');" +
                " INSERT INTO organization  (id, \"name\") VALUES (2, 'Копыта'); " +
                " INSERT INTO organization  (id, \"name\") VALUES (3, 'Лоси'); " +
                " INSERT INTO organization  (id, \"name\") VALUES (4, 'Лебеди'); "
                );
            migrationBuilder.Sql(
                " INSERT INTO municipality  (id, \"name\") VALUES (1, 'Валорант');" +
                " INSERT INTO municipality  (id, \"name\") VALUES (2, 'Апекс'); " +
                " INSERT INTO municipality  (id, \"name\") VALUES (3, 'Дискорд'); " +
                " INSERT INTO municipality  (id, \"name\") VALUES (4, 'Доккер'); "
            );

            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"1", "Оператор ОМСУ"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"2", "Куратор ВетСлужбы"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"3", "Куратор ОМСУ"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"4", "Куратор по отлову"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"5", "Оператор ВетСлужбы"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"6", "Подписант ВетСлужбы"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"7", "Подписант ОМСУ"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"8", "Подписант по отлову"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"9", "Подписант приюта"});
            migrationBuilder.InsertData("role", new[]{"id", "name"}, new[] {"10", "Куратор приюта"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
