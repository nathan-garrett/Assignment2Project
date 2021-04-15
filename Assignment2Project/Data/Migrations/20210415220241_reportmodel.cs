﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment2Project.Data.Migrations
{
    public partial class reportmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportModel",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RType = table.Column<int>(type: "int", nullable: false),
                    IssueDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDTS = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportModel", x => x.ReportId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportModel");
        }
    }
}
