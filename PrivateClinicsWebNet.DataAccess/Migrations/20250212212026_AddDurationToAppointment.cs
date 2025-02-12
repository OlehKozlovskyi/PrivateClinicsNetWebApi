using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateClinicsWebNet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "Duration",
                table: "Appointment",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Appointment");
        }
    }
}
