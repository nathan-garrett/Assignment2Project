using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment2Project.Migrations
{
    public partial class issueid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "UpdateResolve",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "UpdateResolve");
        }
    }
}
