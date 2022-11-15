using Microsoft.EntityFrameworkCore.Migrations;
using PISWF.infrasrtucture.page;

#nullable disable

namespace PISWF.Migrations
{
    public partial class updateregistermcdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var valuesCount = 100;
            var rnd = new Random();
            var registermcs = new string[100, 10];
            var date = Enumerable.Range(0, valuesCount).Select(x => 
                Trash.RndDate(DateTime.Now.AddYears(-2), DateTime.Now).ToString().Substring(0, 10).Split('.')).ToArray();
            var omsys = new[] { "МойкаПомойка", "Самый Главный ОМСУ", "Важный, но не главный ОМСУ", "ОМСУ поселковый"};
            var omsyList = Enumerable.Range(0, valuesCount).Select(x => omsys[rnd.Next(0, 4)]).ToList();
            var locations = new[] { "Улица", "Дом", "Квартира", "Команта" };
            var locationsList = Enumerable.Range(0, valuesCount).Select(x => locations[rnd.Next(0, 4)]).ToList();
            for (int i = 0; i < registermcs.GetLength(0); i++)
            {
                migrationBuilder.Sql(
                    $"UPDATE \"register-m-c\" SET (\"location\", action_date, omsu) = ('{locationsList[i]}', date '{date[i][1]}-{date[i][0]}-{date[i][2]}', " +
                    $"'{omsyList[i]}') WHERE id = {i+1}"
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
