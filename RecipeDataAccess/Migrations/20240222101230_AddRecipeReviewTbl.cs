using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeReviewTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeReviewsTbl",
                columns: table => new
                {
                    RecipeRatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeiD = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeReviewsTbl", x => x.RecipeRatingId);
                    table.ForeignKey(
                        name: "FK_RecipeReviewsTbl_AspNetUsers_ApplicationUserid",
                        column: x => x.ApplicationUserid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeReviewsTbl_RecipeUploadTbl_RecipeiD",
                        column: x => x.RecipeiD,
                        principalTable: "RecipeUploadTbl",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeReviewsTbl_ApplicationUserid",
                table: "RecipeReviewsTbl",
                column: "ApplicationUserid");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeReviewsTbl_RecipeiD",
                table: "RecipeReviewsTbl",
                column: "RecipeiD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeReviewsTbl");
        }
    }
}
