using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Migrations
{
    public partial class D1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Descriptions_DescriptionId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DescriptionId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Descriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_CarId",
                table: "Descriptions",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptions_Cars_CarId",
                table: "Descriptions",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Descriptions_Cars_CarId",
                table: "Descriptions");

            migrationBuilder.DropIndex(
                name: "IX_Descriptions_CarId",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Descriptions");

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DescriptionId",
                table: "Cars",
                column: "DescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Descriptions_DescriptionId",
                table: "Cars",
                column: "DescriptionId",
                principalTable: "Descriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
