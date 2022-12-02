using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class editInsuranceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Doctor_DoctorId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_DoctorId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Insurances");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Insurances",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "DoctorInsurance",
                columns: table => new
                {
                    DoctoresId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsurancesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorInsurance", x => new { x.DoctoresId, x.InsurancesId });
                    table.ForeignKey(
                        name: "FK_DoctorInsurance_Doctor_DoctoresId",
                        column: x => x.DoctoresId,
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
                name: "IX_DoctorInsurance_InsurancesId",
                table: "DoctorInsurance",
                column: "InsurancesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorInsurance");

            migrationBuilder.AlterColumn<int>(
                name: "Discount",
                table: "Insurances",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Insurances",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_DoctorId",
                table: "Insurances",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Doctor_DoctorId",
                table: "Insurances",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");
        }
    }
}
