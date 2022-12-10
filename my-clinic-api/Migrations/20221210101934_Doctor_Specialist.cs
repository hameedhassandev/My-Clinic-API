using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class Doctor_Specialist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialist");

            migrationBuilder.CreateTable(
                name: "Doctor_Specialist",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpecialistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_Specialist", x => new { x.SpecialistId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_Doctor_Specialist_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_Specialist_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Specialist_DoctorId",
                table: "Doctor_Specialist",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor_Specialist");

            migrationBuilder.CreateTable(
                name: "DoctorSpecialist",
                columns: table => new
                {
                    DoctoresId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpecialistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialist", x => new { x.DoctoresId, x.SpecialistsId });
                    table.ForeignKey(
                        name: "FK_DoctorSpecialist_Doctor_DoctoresId",
                        column: x => x.DoctoresId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialist_Specialists_SpecialistsId",
                        column: x => x.SpecialistsId,
                        principalTable: "Specialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialist_SpecialistsId",
                table: "DoctorSpecialist",
                column: "SpecialistsId");
        }
    }
}
