using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefBox.SqlServer.Migrations
{
    public partial class FixingDescriptionColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripton",
                schema: "Cooking",
                table: "Recipes",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "Cooking",
                table: "Recipes",
                newName: "Descripton");
        }
    }
}
