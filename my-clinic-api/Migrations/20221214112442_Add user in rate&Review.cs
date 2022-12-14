using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class AdduserinrateReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RatesAndReviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatesAndReviews_UserId",
                table: "RatesAndReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatesAndReviews_AspNetUsers_UserId",
                table: "RatesAndReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatesAndReviews_AspNetUsers_UserId",
                table: "RatesAndReviews");

            migrationBuilder.DropIndex(
                name: "IX_RatesAndReviews_UserId",
                table: "RatesAndReviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RatesAndReviews");
        }
    }
}
