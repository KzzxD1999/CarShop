using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Migrations
{
    public partial class R1x14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BasketCar",
                columns: table => new
                {
                    BasketId = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketCar", x => new { x.BasketId, x.CarId });
                    table.ForeignKey(
                        name: "FK_BasketCar_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketCar_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketCar_CarId",
                table: "BasketCar",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketCar");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "Cars",
                type: "int",
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
    }
}
