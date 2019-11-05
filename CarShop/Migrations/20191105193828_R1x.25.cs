using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Migrations
{
    public partial class R1x25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarLogoId",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarLogo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameLogo = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarLogo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarLogoId",
                table: "Cars",
                column: "CarLogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarLogo_CarLogoId",
                table: "Cars",
                column: "CarLogoId",
                principalTable: "CarLogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarLogo_CarLogoId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarLogo");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarLogoId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarLogoId",
                table: "Cars");
        }
    }
}
