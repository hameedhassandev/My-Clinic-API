using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class editnameoftaplereportReasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Problems_ProblemId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.RenameColumn(
                name: "ProblemId",
                table: "Reports",
                newName: "ReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ProblemId",
                table: "Reports",
                newName: "IX_Reports_ReasonId");

            migrationBuilder.CreateTable(
                name: "ReportReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReasons", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_ReportReasons_ReasonId",
                table: "Reports",
                column: "ReasonId",
                principalTable: "ReportReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_ReportReasons_ReasonId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "ReportReasons");

            migrationBuilder.RenameColumn(
                name: "ReasonId",
                table: "Reports",
                newName: "ProblemId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ReasonId",
                table: "Reports",
                newName: "IX_Reports_ProblemId");

            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Problems_ProblemId",
                table: "Reports",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
