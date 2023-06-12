using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lennujaam_MVC.Data.Migrations
{
    public partial class lendmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lennud",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KohtadeArv = table.Column<int>(type: "int", nullable: false),
                    ReisijateArv = table.Column<int>(type: "int", nullable: false),
                    Otspunkt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sihtpunkt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValjumisAeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lopetatud = table.Column<bool>(type: "bit", nullable: false),
                    Kestvus = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lennud", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lennud");
        }
    }
}
