using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment2Project.Migrations
{
    public partial class createdbyuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserEmail",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserEmail",
                table: "Reports");
        }
    }
}
