using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class SchoolYears : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolYear",
                table: "Turmas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolYear",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolYear",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "SchoolYear",
                table: "Students");
        }
    }
}
