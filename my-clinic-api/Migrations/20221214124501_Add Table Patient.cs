using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class AddTablePatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatesAndReviews_AspNetUsers_UserId",
                table: "RatesAndReviews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RatesAndReviews",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_RatesAndReviews_UserId",
                table: "RatesAndReviews",
                newName: "IX_RatesAndReviews_PatientId");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RatesAndReviews_Patients_PatientId",
                table: "RatesAndReviews",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatesAndReviews_Patients_PatientId",
                table: "RatesAndReviews");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "RatesAndReviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RatesAndReviews_PatientId",
                table: "RatesAndReviews",
                newName: "IX_RatesAndReviews_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatesAndReviews_AspNetUsers_UserId",
                table: "RatesAndReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
