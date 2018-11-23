using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefBox.SqlServer.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Cooking",
                table: "Recipes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Cooking",
                table: "RecipeIngredients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Cooking",
                table: "Photos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Cooking",
                table: "Ingredients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Cooking",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Cooking",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Cooking",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Cooking",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Cooking",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Cooking",
                table: "Categories");
        }
    }
}
