using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindDal.Migrations
{
    public partial class Email4Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "customers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "customers");
        }
    }
}
