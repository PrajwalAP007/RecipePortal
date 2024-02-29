using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnInRecipeUploadTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "RecipeUploadTbl",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "RecipeUploadTbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "RecipeUploadTbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "RecipeUploadTbl",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "RecipeUploadTbl");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "RecipeUploadTbl");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "RecipeUploadTbl");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "RecipeUploadTbl");
        }
    }
}
