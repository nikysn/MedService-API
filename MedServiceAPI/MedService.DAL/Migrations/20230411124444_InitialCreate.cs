using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                 name: "PatientId",
                 table: "AppointmentTimes",
                 type: "int",
                 nullable: false,
                 defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTimes_PatientId",
                table: "AppointmentTimes",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentTimes_Patients_PatientId",
                table: "AppointmentTimes",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_AppointmentTimes_Patients_PatientId",
               table: "AppointmentTimes");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentTimes_PatientId",
                table: "AppointmentTimes");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "AppointmentTimes");
        }
    }
}
