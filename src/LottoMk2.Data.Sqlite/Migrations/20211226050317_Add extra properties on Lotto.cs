using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LottoMk2.Data.Sqlite.Migrations
{
    public partial class AddextrapropertiesonLotto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirstPrizeWinners",
                table: "Lotto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirstWinningAmounts",
                table: "Lotto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalRewards",
                table: "Lotto",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPrizeWinners",
                table: "Lotto");

            migrationBuilder.DropColumn(
                name: "FirstWinningAmounts",
                table: "Lotto");

            migrationBuilder.DropColumn(
                name: "TotalRewards",
                table: "Lotto");
        }
    }
}
