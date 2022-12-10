using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class M2M_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorHospital");

            migrationBuilder.DropTable(
                name: "DoctorInsurance");

            migrationBuilder.CreateTable(
                name: "Doctor_Hospital",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_Hospital", x => new { x.HospitalId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_Doctor_Hospital_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_Hospital_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor_Insurance",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_Insurance", x => new { x.InsuranceId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_Doctor_Insurance_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_Insurance_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Hospital_DoctorId",
                table: "Doctor_Hospital",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Insurance_DoctorId",
                table: "Doctor_Insurance",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor_Hospital");

            migrationBuilder.DropTable(
                name: "Doctor_Insurance");

            migrationBuilder.CreateTable(
                name: "DoctorHospital",
                columns: table => new
                {
                    HospitalsId = table.Column<int>(type: "int", nullable: false),
                    doctorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorHospital", x => new { x.HospitalsId, x.doctorsId });
                    table.ForeignKey(
                        name: "FK_DoctorHospital_Doctor_doctorsId",
                        column: x => x.doctorsId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorHospital_Hospitals_HospitalsId",
                        column: x => x.HospitalsId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorInsurance",
                columns: table => new
                {
                    InsurancesId = table.Column<int>(type: "int", nullable: false),
                    doctorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorInsurance", x => new { x.InsurancesId, x.doctorsId });
                    table.ForeignKey(
                        name: "FK_DoctorInsurance_Doctor_doctorsId",
                        column: x => x.doctorsId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorInsurance_Insurances_InsurancesId",
                        column: x => x.InsurancesId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorHospital_doctorsId",
                table: "DoctorHospital",
                column: "doctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInsurance_doctorsId",
                table: "DoctorInsurance",
                column: "doctorsId");
        }
    }
}
