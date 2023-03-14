using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateAppointmentTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupied",
                table: "AppointmentTimes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Occupied",
                table: "AppointmentTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
