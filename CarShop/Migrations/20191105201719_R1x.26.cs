using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Migrations
{
    public partial class R1x26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarLogo_CarLogoId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarLogo",
                table: "CarLogo");

            migrationBuilder.RenameTable(
                name: "CarLogo",
                newName: "CarLogos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarLogos",
                table: "CarLogos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarLogos_CarLogoId",
                table: "Cars",
                column: "CarLogoId",
                principalTable: "CarLogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarLogos_CarLogoId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarLogos",
                table: "CarLogos");

            migrationBuilder.RenameTable(
                name: "CarLogos",
                newName: "CarLogo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarLogo",
                table: "CarLogo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarLogo_CarLogoId",
                table: "Cars",
                column: "CarLogoId",
                principalTable: "CarLogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
