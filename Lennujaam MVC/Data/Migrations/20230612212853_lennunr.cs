using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lennujaam_MVC.Data.Migrations
{
    public partial class lennunr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LennuNR",
                table: "Lennud",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LennuNR",
                table: "Lennud");
        }
    }
}
