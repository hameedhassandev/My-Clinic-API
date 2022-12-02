using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class editDoctorAndSpicialistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Doctor_DoctorId",
                table: "Specialists");

            migrationBuilder.DropIndex(
                name: "IX_Specialists_DoctorId",
                table: "Specialists");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Specialists");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialist");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Specialists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_DoctorId",
                table: "Specialists",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Doctor_DoctorId",
                table: "Specialists",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");
        }
    }
}
