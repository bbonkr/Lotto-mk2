using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LottoMk2.Data.Sqlite.Migrations
{
    public partial class Initializedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lotto",
                columns: table => new
                {
                    Round = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LotteryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Num1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Num2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Num3 = table.Column<int>(type: "INTEGER", nullable: false),
                    Num4 = table.Column<int>(type: "INTEGER", nullable: false),
                    Num5 = table.Column<int>(type: "INTEGER", nullable: false),
                    Num6 = table.Column<int>(type: "INTEGER", nullable: false),
                    NumBonus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotto", x => x.Round);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotto");
        }
    }
}
