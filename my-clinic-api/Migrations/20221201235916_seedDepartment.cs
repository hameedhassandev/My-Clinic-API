using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class seedDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name", "Description", },
               values: new object[] { 1, "Allergist", "An allergist is a healthcare professional who specializes in identifying and treating asthma" }

           );
            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name", "Description", },
               values: new object[] { 2, "Anaesthesiologist", "Anaesthesiologists play a major decisive role in pain management." }

           );
            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name", "Description", },
               values: new object[] { 3, "Andrologist", "An Andrologist is the male equivalent of a gynecologist" }

           );
            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name", "Description", },
               values: new object[] { 4, "Cardiologist", "Cardiologists are medical professionals that examine and treat illnesses associated with the cardiovascular system which includes the heart and blood vessels." }

           );
            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name", "Description", },
               values: new object[] { 5, "Obstetrics and gynecology", "Obstetrics and Gynaecology is the medical specialty that encompasses the two subspecialties of obstetrics and gynecology" }

           );
            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name", "Description", },
               values: new object[] { 6, "Internal medicine", "Internal medicine is the study, diagnosis, and treatment of conditions that affect the internal organs" }

           );
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Departments]");

        }
    }
}
