using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DContinentId",
                table: "Countries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_DContinentId",
                table: "Countries",
                column: "DContinentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Continents_DContinentId",
                table: "Countries",
                column: "DContinentId",
                principalTable: "Continents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Continents_DContinentId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_DContinentId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DContinentId",
                table: "Countries");
        }
    }
}
