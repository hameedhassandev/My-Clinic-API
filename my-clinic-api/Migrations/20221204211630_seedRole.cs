using Microsoft.EntityFrameworkCore.Migrations;
using my_clinic_api.Classes;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class seedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), RoleNames.AdminRole, RoleNames.AdminRole.ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), RoleNames.DoctorRole, RoleNames.DoctorRole.ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), RoleNames.PatientRole, RoleNames.PatientRole.ToUpper(), Guid.NewGuid().ToString() }
            );
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles]");

        }
    }
}
