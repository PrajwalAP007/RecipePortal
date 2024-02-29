using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForiengKeyToUserRecordApplicationuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserid",
                table: "UserRecordsTbl",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRecordsTbl_ApplicationUserid",
                table: "UserRecordsTbl",
                column: "ApplicationUserid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecordsTbl_AspNetUsers_ApplicationUserid",
                table: "UserRecordsTbl",
                column: "ApplicationUserid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecordsTbl_AspNetUsers_ApplicationUserid",
                table: "UserRecordsTbl");

            migrationBuilder.DropIndex(
                name: "IX_UserRecordsTbl_ApplicationUserid",
                table: "UserRecordsTbl");

            migrationBuilder.DropColumn(
                name: "ApplicationUserid",
                table: "UserRecordsTbl");
        }
    }
}
