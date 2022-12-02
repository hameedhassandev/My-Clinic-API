using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_clinic_api.Migrations
{
    public partial class seedSpecialists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 1, "asthma", 1 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 2, "Allergic rhinitis", 1 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 3, "hay fever" , 1 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 4, "Critical Care Anesthesia", 2 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 5, "Neurosurgical Anesthesia", 2 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 6, "Obstetric Anesthesia", 2 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 7, "Varicocele", 3 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] {8, "Vasectomy", 3 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] {9, "Undescended Testicle", 3 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] {10, "Electrocardiography", 4 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] {11, "Geriatrics", 4 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 12, "Echocardiography", 4 }

                    );

                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 13, "Interventional Cardiology", 4 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 14, "Gynecologic oncology", 5 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 15, "Pelvis and surgery", 5 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] {16, "Hypogonadism", 5 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 17, "Metabolic syndrome", 6 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 18, "blood vessels", 6 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] {19, "Bronchial", 6 }

                    );
                    migrationBuilder.InsertData(
                    table: "Specialists",
                    columns: new[] { "Id", "SpecialistName", "departmentId", },
                    values: new object[] { 20, "Oncology", 6 }

                    );



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                  migrationBuilder.Sql("DELETE FROM [dbo].[Specialists]");
        }
    }
}
