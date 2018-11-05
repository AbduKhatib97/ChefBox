using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefBox.SqlServer.Migrations
{
    public partial class NewChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                schema: "Cooking",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Cooking",
                table: "Photos",
                newName: "Description");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "Cooking",
                table: "Recipes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                schema: "Cooking",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IngredientType",
                schema: "Cooking",
                table: "Ingredients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "Cooking",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientType",
                schema: "Cooking",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "Cooking",
                table: "Photos",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                schema: "Cooking",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                schema: "Cooking",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
