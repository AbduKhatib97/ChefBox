using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefBox.SqlServer.Migrations
{
    public partial class AddIsMainPropertyIntoPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                schema: "Cooking",
                table: "Photos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCover",
                schema: "Cooking",
                table: "Photos");
        }
    }
}
