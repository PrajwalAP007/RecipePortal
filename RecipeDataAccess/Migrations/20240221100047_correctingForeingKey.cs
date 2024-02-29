using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class correctingForeingKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecordsTbl_UserRecordsTbl_RecipeiD",
                table: "UserRecordsTbl");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecordsTbl_RecipeUploadTbl_RecipeiD",
                table: "UserRecordsTbl",
                column: "RecipeiD",
                principalTable: "RecipeUploadTbl",
                principalColumn: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecordsTbl_RecipeUploadTbl_RecipeiD",
                table: "UserRecordsTbl");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecordsTbl_UserRecordsTbl_RecipeiD",
                table: "UserRecordsTbl",
                column: "RecipeiD",
                principalTable: "UserRecordsTbl",
                principalColumn: "UserRecordId");
        }
    }
}
