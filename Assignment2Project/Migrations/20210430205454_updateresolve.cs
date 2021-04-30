using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment2Project.Migrations
{
    public partial class updateresolve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpdateResolutionModel",
                columns: table => new
                {
                    UpdateResolveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffMemberActioning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateResolveDTS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportModelReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateResolutionModel", x => x.UpdateResolveId);
                    table.ForeignKey(
                        name: "FK_UpdateResolutionModel_Reports_ReportModelReportId",
                        column: x => x.ReportModelReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpdateResolutionModel_ReportModelReportId",
                table: "UpdateResolutionModel",
                column: "ReportModelReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpdateResolutionModel");
        }
    }
}
