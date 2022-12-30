using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class editcollectiondoctorsinspecialist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialist_Doctor_DoctoresId",
                table: "DoctorSpecialist");

            migrationBuilder.RenameColumn(
                name: "DoctoresId",
                table: "DoctorSpecialist",
                newName: "DoctorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialist_Doctor_DoctorsId",
                table: "DoctorSpecialist",
                column: "DoctorsId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialist_Doctor_DoctorsId",
                table: "DoctorSpecialist");

            migrationBuilder.RenameColumn(
                name: "DoctorsId",
                table: "DoctorSpecialist",
                newName: "DoctoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialist_Doctor_DoctoresId",
                table: "DoctorSpecialist",
                column: "DoctoresId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
