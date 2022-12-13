using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class AddedMarksToGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mark",
                table: "Grades",
                newName: "ThirdMark");

            migrationBuilder.AddColumn<double>(
                name: "FirstMark",
                table: "Grades",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SecondMark",
                table: "Grades",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstMark",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SecondMark",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "ThirdMark",
                table: "Grades",
                newName: "Mark");
        }
    }
}
