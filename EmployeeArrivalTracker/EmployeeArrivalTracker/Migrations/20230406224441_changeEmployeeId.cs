﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeArrivalTracker.Migrations
{
    /// <inheritdoc />
    public partial class changeEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");
        }
    }
}
