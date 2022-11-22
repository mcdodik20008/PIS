using Microsoft.EntityFrameworkCore.Migrations;
using PISWF.infrasrtucture.page;

#nullable disable

namespace PISWF.Migrations
{
    public partial class addregistermcdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var valuesCount = 1000;
            var rnd = new Random();
            var registermcs = new string[valuesCount, 10];
            var id = Enumerable.Range(1, valuesCount).Select(x => x.ToString()).ToArray();
            var numbers = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(0, 1000).ToString()).ToArray();
            var date = Enumerable.Range(0, valuesCount).Select(x => 
                Trash.RndDate(DateTime.Now.AddYears(-2), DateTime.Now).ToString().Substring(0, 10).Split('.')).ToArray();
            var organizations = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(1, 5).ToString()).ToArray();
            var municipality = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(1, 5).ToString()).ToArray();
            var year = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(2010, 2022).ToString()).ToArray();
            var price = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(100000, 5000000).ToString()).ToArray();
            var subvention_share = Enumerable.Range(0, valuesCount).Select(x => new Random(x).Next(100000, 5000000).ToString()).ToArray();
            var amount_money = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(100000, 5000000).ToString()).ToArray();
            var share_funds_subvention = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(100000, 5000000).ToString()).ToArray();
            for (int i = 0; i < registermcs.GetLength(0); i++)
            {
                migrationBuilder.Sql(
                    $"INSERT INTO \"register-m-c\" VALUES ({id[i]}, " +
                    $"{numbers[i]}, '{date[i][1]}-{date[i][0]}-{date[i][2]}', {organizations[i]}, {municipality[i]}, " +
                    $"{year[i]}, {price[i]}, {subvention_share[i]}, {amount_money[i]}, " +
                    $"{share_funds_subvention[i]})"
                    );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
