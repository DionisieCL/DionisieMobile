using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class AddTurmaToLessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TurmaId",
                table: "Lessons",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Turma_TurmaId",
                table: "Lessons",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Turma_TurmaId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TurmaId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Lessons");
        }
    }
}
