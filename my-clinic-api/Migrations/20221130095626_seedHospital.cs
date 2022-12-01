using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class seedHospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] {5, "Anglo American Hospital", "Zamalek, Cairo" }
             
            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 6, "Arab Contractor Hospital", "Al Gabal Al Akhdar, Cairo" }

            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 7, "Behman Hospital", "Maadi, Cairo" }

            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 8, "Cairo Medical Hospital", "Heliopolis, Cairo" }

            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 9, "Cairo Radiology Center (Mohandisen)", "Cairo Radiology Center (Giza)" }

            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 10, "Cardiac Center", "Heliopolis, Cairo" }

            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 11, "Damascus Hospital", "Dokki, Giza" }

            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 12, "Dar El Fouad Hospital", "Dar El Fouad Hospital (6th of October City, Cairo)" }

            );

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Name", "Address", },
                values: new object[] { 13, "Degla Medical Center", "Rd 203 Bldg 4, Degla, Maadi" }

            );


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Hospitals]");
        }
    }
}
