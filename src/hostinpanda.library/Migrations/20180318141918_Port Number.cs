using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace hostinpanda.library.Migrations
{
    public partial class PortNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowableDowntimeMinutes",
                table: "Hosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortNumber",
                table: "Hosts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowableDowntimeMinutes",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "PortNumber",
                table: "Hosts");
        }
    }
}
