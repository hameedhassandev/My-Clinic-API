using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class EditdoctorsinInsuranceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorInsurance_Doctor_DoctoresId",
                table: "DoctorInsurance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorInsurance",
                table: "DoctorInsurance");

            migrationBuilder.DropIndex(
                name: "IX_DoctorInsurance_InsurancesId",
                table: "DoctorInsurance");

            migrationBuilder.RenameColumn(
                name: "DoctoresId",
                table: "DoctorInsurance",
                newName: "doctorsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorInsurance",
                table: "DoctorInsurance",
                columns: new[] { "InsurancesId", "doctorsId" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInsurance_doctorsId",
                table: "DoctorInsurance",
                column: "doctorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorInsurance_Doctor_doctorsId",
                table: "DoctorInsurance",
                column: "doctorsId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorInsurance_Doctor_doctorsId",
                table: "DoctorInsurance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorInsurance",
                table: "DoctorInsurance");

            migrationBuilder.DropIndex(
                name: "IX_DoctorInsurance_doctorsId",
                table: "DoctorInsurance");

            migrationBuilder.RenameColumn(
                name: "doctorsId",
                table: "DoctorInsurance",
                newName: "DoctoresId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorInsurance",
                table: "DoctorInsurance",
                columns: new[] { "DoctoresId", "InsurancesId" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInsurance_InsurancesId",
                table: "DoctorInsurance",
                column: "InsurancesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorInsurance_Doctor_DoctoresId",
                table: "DoctorInsurance",
                column: "DoctoresId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
