using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment2Project.Migrations
{
    public partial class updatingdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpdateResolutionModel_Reports_ReportModelReportId",
                table: "UpdateResolutionModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UpdateResolutionModel",
                table: "UpdateResolutionModel");

            migrationBuilder.RenameTable(
                name: "UpdateResolutionModel",
                newName: "UpdateResolve");

            migrationBuilder.RenameIndex(
                name: "IX_UpdateResolutionModel_ReportModelReportId",
                table: "UpdateResolve",
                newName: "IX_UpdateResolve_ReportModelReportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UpdateResolve",
                table: "UpdateResolve",
                column: "UpdateResolveId");

            migrationBuilder.AddForeignKey(
                name: "FK_UpdateResolve_Reports_ReportModelReportId",
                table: "UpdateResolve",
                column: "ReportModelReportId",
                principalTable: "Reports",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpdateResolve_Reports_ReportModelReportId",
                table: "UpdateResolve");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UpdateResolve",
                table: "UpdateResolve");

            migrationBuilder.RenameTable(
                name: "UpdateResolve",
                newName: "UpdateResolutionModel");

            migrationBuilder.RenameIndex(
                name: "IX_UpdateResolve_ReportModelReportId",
                table: "UpdateResolutionModel",
                newName: "IX_UpdateResolutionModel_ReportModelReportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UpdateResolutionModel",
                table: "UpdateResolutionModel",
                column: "UpdateResolveId");

            migrationBuilder.AddForeignKey(
                name: "FK_UpdateResolutionModel_Reports_ReportModelReportId",
                table: "UpdateResolutionModel",
                column: "ReportModelReportId",
                principalTable: "Reports",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
