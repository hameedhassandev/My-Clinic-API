using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class addDepartmentIdintablespecialist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Departments_departmentId",
                table: "Specialists");

            migrationBuilder.AlterColumn<int>(
                name: "departmentId",
                table: "Specialists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Departments_departmentId",
                table: "Specialists",
                column: "departmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialists_Departments_departmentId",
                table: "Specialists");

            migrationBuilder.AlterColumn<int>(
                name: "departmentId",
                table: "Specialists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialists_Departments_departmentId",
                table: "Specialists",
                column: "departmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
