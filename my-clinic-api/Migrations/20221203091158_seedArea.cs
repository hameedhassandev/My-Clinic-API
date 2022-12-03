using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class seedArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                   table: "Areas",
                   columns: new[] { "Id", "City", "AreaName", },
                   values: new object[] { 1, 0, "Coptic Cairo" }

                   );
            migrationBuilder.InsertData(
                   table: "Areas",
                   columns: new[] { "Id", "City", "AreaName", },
                   values: new object[] { 2, 0, "El Matareya" }

                   );


            migrationBuilder.InsertData(
            table: "Areas",
            columns: new[] { "Id", "City", "AreaName", },
            values: new object[] { 3, 0, "Maadi" }

            );
            migrationBuilder.InsertData(
           table: "Areas",
           columns: new[] { "Id", "City", "AreaName", },
           values: new object[] { 4, 1, "Mansheya" }

           );
            migrationBuilder.InsertData(
           table: "Areas",
           columns: new[] { "Id", "City", "AreaName", },
           values: new object[] { 5, 1, "El Raml 1" }

           );
            migrationBuilder.InsertData(
           table: "Areas",
           columns: new[] { "Id", "City", "AreaName", },
           values: new object[] { 6, 1, "Borg El Arab" }

           );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Areas]");

        }
    }
}
