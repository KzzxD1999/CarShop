using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Migrations
{
    public partial class R1x19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketCar_Baskets_BasketId",
                table: "BasketCar");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketCar_Cars_CarId",
                table: "BasketCar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketCar",
                table: "BasketCar");

            migrationBuilder.RenameTable(
                name: "BasketCar",
                newName: "BasketCars");

            migrationBuilder.RenameIndex(
                name: "IX_BasketCar_CarId",
                table: "BasketCars",
                newName: "IX_BasketCars_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketCars",
                table: "BasketCars",
                columns: new[] { "BasketId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketCars_Baskets_BasketId",
                table: "BasketCars",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketCars_Cars_CarId",
                table: "BasketCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketCars_Baskets_BasketId",
                table: "BasketCars");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketCars_Cars_CarId",
                table: "BasketCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketCars",
                table: "BasketCars");

            migrationBuilder.RenameTable(
                name: "BasketCars",
                newName: "BasketCar");

            migrationBuilder.RenameIndex(
                name: "IX_BasketCars_CarId",
                table: "BasketCar",
                newName: "IX_BasketCar_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketCar",
                table: "BasketCar",
                columns: new[] { "BasketId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketCar_Baskets_BasketId",
                table: "BasketCar",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketCar_Cars_CarId",
                table: "BasketCar",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
