using Microsoft.EntityFrameworkCore.Migrations;

namespace AkijGroup.Migrations
{
    public partial class UpdateColdDrinksNameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColdDrinksName",
                table: "ColdDrinks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ColdDrinks_ColdDrinksName",
                table: "ColdDrinks",
                column: "ColdDrinksName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ColdDrinks_ColdDrinksName",
                table: "ColdDrinks");

            migrationBuilder.AlterColumn<string>(
                name: "ColdDrinksName",
                table: "ColdDrinks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
