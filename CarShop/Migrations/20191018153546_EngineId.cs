using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Migrations
{
    public partial class EngineId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Engine_EngineId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engine",
                table: "Engine");

            migrationBuilder.RenameTable(
                name: "Engine",
                newName: "Engines");

            migrationBuilder.AlterColumn<int>(
                name: "EngineId",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engines",
                table: "Engines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engines",
                table: "Engines");

            migrationBuilder.RenameTable(
                name: "Engines",
                newName: "Engine");

            migrationBuilder.AlterColumn<int>(
                name: "EngineId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engine",
                table: "Engine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Engine_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "Engine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
