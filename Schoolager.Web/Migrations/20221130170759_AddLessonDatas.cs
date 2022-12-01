using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class AddLessonDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonData_Lessons_LessonId",
                table: "LessonData");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsData_LessonData_LessonDataId",
                table: "StudentLessonsData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonData",
                table: "LessonData");

            migrationBuilder.RenameTable(
                name: "LessonData",
                newName: "LessonDatas");

            migrationBuilder.RenameIndex(
                name: "IX_LessonData_LessonId",
                table: "LessonDatas",
                newName: "IX_LessonDatas_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonDatas",
                table: "LessonDatas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonDatas_Lessons_LessonId",
                table: "LessonDatas",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsData_LessonDatas_LessonDataId",
                table: "StudentLessonsData",
                column: "LessonDataId",
                principalTable: "LessonDatas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonDatas_Lessons_LessonId",
                table: "LessonDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsData_LessonDatas_LessonDataId",
                table: "StudentLessonsData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonDatas",
                table: "LessonDatas");

            migrationBuilder.RenameTable(
                name: "LessonDatas",
                newName: "LessonData");

            migrationBuilder.RenameIndex(
                name: "IX_LessonDatas_LessonId",
                table: "LessonData",
                newName: "IX_LessonData_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonData",
                table: "LessonData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonData_Lessons_LessonId",
                table: "LessonData",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsData_LessonData_LessonDataId",
                table: "StudentLessonsData",
                column: "LessonDataId",
                principalTable: "LessonData",
                principalColumn: "Id");
        }
    }
}
