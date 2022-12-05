using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class FixDeleteBehaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsDatas_LessonDatas_LessonDataId",
                table: "StudentLessonsDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsDatas_Students_StudentId",
                table: "StudentLessonsDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Turmas_TurmaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurmas_Subjects_SubjectId",
                table: "SubjectTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurmas_Turmas_TurmaId",
                table: "SubjectTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherTurmas_Teachers_TeacherId",
                table: "TeacherTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherTurmas_Turmas_TurmaId",
                table: "TeacherTurmas");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsDatas_LessonDatas_LessonDataId",
                table: "StudentLessonsDatas",
                column: "LessonDataId",
                principalTable: "LessonDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsDatas_Students_StudentId",
                table: "StudentLessonsDatas",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Turmas_TurmaId",
                table: "Students",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurmas_Subjects_SubjectId",
                table: "SubjectTurmas",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurmas_Turmas_TurmaId",
                table: "SubjectTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherTurmas_Teachers_TeacherId",
                table: "TeacherTurmas",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherTurmas_Turmas_TurmaId",
                table: "TeacherTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsDatas_LessonDatas_LessonDataId",
                table: "StudentLessonsDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsDatas_Students_StudentId",
                table: "StudentLessonsDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Turmas_TurmaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurmas_Subjects_SubjectId",
                table: "SubjectTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurmas_Turmas_TurmaId",
                table: "SubjectTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherTurmas_Teachers_TeacherId",
                table: "TeacherTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherTurmas_Turmas_TurmaId",
                table: "TeacherTurmas");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsDatas_LessonDatas_LessonDataId",
                table: "StudentLessonsDatas",
                column: "LessonDataId",
                principalTable: "LessonDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsDatas_Students_StudentId",
                table: "StudentLessonsDatas",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Turmas_TurmaId",
                table: "Students",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurmas_Subjects_SubjectId",
                table: "SubjectTurmas",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurmas_Turmas_TurmaId",
                table: "SubjectTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherTurmas_Teachers_TeacherId",
                table: "TeacherTurmas",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherTurmas_Turmas_TurmaId",
                table: "TeacherTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id");
        }
    }
}
