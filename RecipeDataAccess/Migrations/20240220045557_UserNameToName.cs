using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserNameToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeUploadTbl_AspNetUsers_ApplicationUserid",
                table: "RecipeUploadTbl");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserid",
                table: "RecipeUploadTbl",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeUploadTbl_AspNetUsers_ApplicationUserid",
                table: "RecipeUploadTbl",
                column: "ApplicationUserid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeUploadTbl_AspNetUsers_ApplicationUserid",
                table: "RecipeUploadTbl");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserid",
                table: "RecipeUploadTbl",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeUploadTbl_AspNetUsers_ApplicationUserid",
                table: "RecipeUploadTbl",
                column: "ApplicationUserid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
