using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingDayDal.Migrations
{
    public partial class FlagSymbolAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Flagsymbol",
                table: "ExchangeRates",
                type: "bytea",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flagsymbol",
                table: "ExchangeRates");
        }
    }
}
