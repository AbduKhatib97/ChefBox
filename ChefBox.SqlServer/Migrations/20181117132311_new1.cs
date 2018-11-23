using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefBox.SqlServer.Migrations
{
    public partial class new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Recipes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                schema: "Cooking",
                table: "RecipeIngredients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Photos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Ingredients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                schema: "Cooking",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                schema: "Cooking",
                table: "Categories");
        }
    }
}
