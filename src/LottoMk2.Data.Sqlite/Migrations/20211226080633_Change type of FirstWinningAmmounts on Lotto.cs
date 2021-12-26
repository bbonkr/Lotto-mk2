using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LottoMk2.Data.Sqlite.Migrations
{
    public partial class ChangetypeofFirstWinningAmmountsonLotto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FirstWinningAmounts",
                table: "Lotto",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FirstWinningAmounts",
                table: "Lotto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
