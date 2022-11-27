using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class doctorsAndNP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Departments_DepartmentId",
                table: "Specialists");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "TimesOfWork",
                newName: "doctorId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Specialists",
                newName: "departmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Specialists_DepartmentId",
                table: "Specialists",
                newName: "IX_Specialists_departmentId");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "RatesAndReviews",
                newName: "doctorId");

            migrationBuilder.AlterColumn<string>(
                name: "doctorId",
                table: "TimesOfWork",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "departmentId",
                table: "Specialists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Specialists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "RatesAndReviews",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "doctorId",
                table: "RatesAndReviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Insurances",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorTitle = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    AvgRate = table.Column<float>(type: "real", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NumberOfViews = table.Column<int>(type: "int", nullable: false),
                    WaitingTime = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctor_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TimesOfWork_doctorId",
                table: "TimesOfWork",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_DoctorId",
                table: "Specialists",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_RatesAndReviews_doctorId",
                table: "RatesAndReviews",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_DoctorId",
                table: "Insurances",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DepartmentId",
                table: "Doctor",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorHospital_doctorsId",
                table: "DoctorHospital",
                column: "doctorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Doctor_DoctorId",
                table: "Insurances",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatesAndReviews_Doctor_doctorId",
                table: "RatesAndReviews",
                column: "doctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Departments_departmentId",
                table: "Specialists",
                column: "departmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Doctor_DoctorId",
                table: "Specialists",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesOfWork_Doctor_doctorId",
                table: "TimesOfWork",
                column: "doctorId",
                principalTable: "Doctor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Doctor_DoctorId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_RatesAndReviews_Doctor_doctorId",
                table: "RatesAndReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Departments_departmentId",
                table: "Specialists");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Doctor_DoctorId",
                table: "Specialists");

            migrationBuilder.DropForeignKey(
                name: "FK_TimesOfWork_Doctor_doctorId",
                table: "TimesOfWork");

            migrationBuilder.DropTable(
                name: "DoctorHospital");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_TimesOfWork_doctorId",
                table: "TimesOfWork");

            migrationBuilder.DropIndex(
                name: "IX_Specialists_DoctorId",
                table: "Specialists");

            migrationBuilder.DropIndex(
                name: "IX_RatesAndReviews_doctorId",
                table: "RatesAndReviews");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_DoctorId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Specialists");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Insurances");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "TimesOfWork",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "departmentId",
                table: "Specialists",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Specialists_departmentId",
                table: "Specialists",
                newName: "IX_Specialists_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "RatesAndReviews",
                newName: "DoctorId");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "TimesOfWork",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Specialists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "RatesAndReviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "RatesAndReviews",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Departments_DepartmentId",
                table: "Specialists",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
