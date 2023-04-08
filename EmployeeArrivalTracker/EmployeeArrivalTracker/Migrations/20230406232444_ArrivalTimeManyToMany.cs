using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeArrivalTracker.Migrations
{
    /// <inheritdoc />
    public partial class ArrivalTimeManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "ArrivalInformation");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "ArrivalInformation",
                newName: "When");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "When",
                table: "ArrivalInformation",
                newName: "DepartureDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "ArrivalInformation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
