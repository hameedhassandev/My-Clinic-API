using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class seedInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 1, "AXA Insurance", 20 }

            );
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 2, "Metlife Alico", 10.5 }

            );
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 3, "Misr Insurance", 13 }

            );
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 4, "Allianz", 9 }

            );
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 5, "Suez Canal Insurance", 30 }

            );
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 6, "Delta insurance", 12 }

            );
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 7, "CHUBB", 5 }

            );
            migrationBuilder.InsertData(
            table: "Insurances",
            columns: new[] { "Id", "CompanyName", "Discount", },
            values: new object[] { 8, "GIG", 11 }

            );


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Insurances]");

        }
    }
}
