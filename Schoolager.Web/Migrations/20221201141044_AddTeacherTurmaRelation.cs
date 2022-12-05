using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class AddTeacherTurmaRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Turma_TurmaId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsData_LessonDatas_LessonDataId",
                table: "StudentLessonsData");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsData_Students_StudentId",
                table: "StudentLessonsData");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Turma_TurmaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurmas_Turma_TurmaId",
                table: "SubjectTurmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turma",
                table: "Turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLessonsData",
                table: "StudentLessonsData");

            migrationBuilder.RenameTable(
                name: "Turma",
                newName: "Turmas");

            migrationBuilder.RenameTable(
                name: "StudentLessonsData",
                newName: "StudentLessonsDatas");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessonsData_LessonDataId",
                table: "StudentLessonsDatas",
                newName: "IX_StudentLessonsDatas_LessonDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLessonsDatas",
                table: "StudentLessonsDatas",
                columns: new[] { "StudentId", "LessonDataId" });

            migrationBuilder.CreateTable(
                name: "TeacherTurmas",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherTurmas", x => new { x.TeacherId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_TeacherTurmas_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherTurmas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTurmas_TurmaId",
                table: "TeacherTurmas",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Turmas_TurmaId",
                table: "Lessons",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_SubjectTurmas_Turmas_TurmaId",
                table: "SubjectTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Turmas_TurmaId",
                table: "Lessons");

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
                name: "FK_SubjectTurmas_Turmas_TurmaId",
                table: "SubjectTurmas");

            migrationBuilder.DropTable(
                name: "TeacherTurmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLessonsDatas",
                table: "StudentLessonsDatas");

            migrationBuilder.RenameTable(
                name: "Turmas",
                newName: "Turma");

            migrationBuilder.RenameTable(
                name: "StudentLessonsDatas",
                newName: "StudentLessonsData");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessonsDatas_LessonDataId",
                table: "StudentLessonsData",
                newName: "IX_StudentLessonsData_LessonDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turma",
                table: "Turma",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLessonsData",
                table: "StudentLessonsData",
                columns: new[] { "StudentId", "LessonDataId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Turma_TurmaId",
                table: "Lessons",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsData_LessonDatas_LessonDataId",
                table: "StudentLessonsData",
                column: "LessonDataId",
                principalTable: "LessonDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonsData_Students_StudentId",
                table: "StudentLessonsData",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Turma_TurmaId",
                table: "Students",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurmas_Turma_TurmaId",
                table: "SubjectTurmas",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id");
        }
    }
}
