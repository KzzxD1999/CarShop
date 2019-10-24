using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Migrations
{
    public partial class R1x9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BasketId",
                table: "Cars",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Baskets_BasketId",
                table: "Cars",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Baskets_BasketId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BasketId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Cars");
        }
    }
}
