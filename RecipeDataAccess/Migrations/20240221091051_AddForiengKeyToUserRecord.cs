using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForiengKeyToUserRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "UserRecordsTbl",
                newName: "RecipeiD");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecordsTbl_RecipeiD",
                table: "UserRecordsTbl",
                column: "RecipeiD");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecordsTbl_UserRecordsTbl_RecipeiD",
                table: "UserRecordsTbl",
                column: "RecipeiD",
                principalTable: "UserRecordsTbl",
                principalColumn: "UserRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecordsTbl_UserRecordsTbl_RecipeiD",
                table: "UserRecordsTbl");

            migrationBuilder.DropIndex(
                name: "IX_UserRecordsTbl_RecipeiD",
                table: "UserRecordsTbl");

            migrationBuilder.RenameColumn(
                name: "RecipeiD",
                table: "UserRecordsTbl",
                newName: "RecipeId");
        }
    }
}
