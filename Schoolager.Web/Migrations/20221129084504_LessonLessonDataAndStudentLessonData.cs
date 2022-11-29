using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class LessonLessonDataAndStudentLessonData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurma_Subjects_SubjectId",
                table: "SubjectTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurma_Turma_TurmaId",
                table: "SubjectTurma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTurma",
                table: "SubjectTurma");

            migrationBuilder.RenameTable(
                name: "SubjectTurma",
                newName: "SubjectTurmas");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Teachers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teachers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Teachers",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Students",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Students",
                newName: "DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectTurma_TurmaId",
                table: "SubjectTurmas",
                newName: "IX_SubjectTurmas_TurmaId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTurmas",
                table: "SubjectTurmas",
                columns: new[] { "SubjectId", "TurmaId" });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecurrenceRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecurrenceException = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonData_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessonsData",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LessonDataId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    WasPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessonsData", x => new { x.StudentId, x.LessonDataId });
                    table.ForeignKey(
                        name: "FK_StudentLessonsData_LessonData_LessonDataId",
                        column: x => x.LessonDataId,
                        principalTable: "LessonData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentLessonsData_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonData_LessonId",
                table: "LessonData",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TeacherId",
                table: "Lessons",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonsData_LessonDataId",
                table: "StudentLessonsData",
                column: "LessonDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurmas_Subjects_SubjectId",
                table: "SubjectTurmas",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurmas_Turma_TurmaId",
                table: "SubjectTurmas",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurmas_Subjects_SubjectId",
                table: "SubjectTurmas");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTurmas_Turma_TurmaId",
                table: "SubjectTurmas");

            migrationBuilder.DropTable(
                name: "StudentLessonsData");

            migrationBuilder.DropTable(
                name: "LessonData");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTurmas",
                table: "SubjectTurmas");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "SubjectTurmas",
                newName: "SubjectTurma");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Teachers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Teachers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Teachers",
                newName: "Birthday");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Students",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Students",
                newName: "Birthday");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectTurmas_TurmaId",
                table: "SubjectTurma",
                newName: "IX_SubjectTurma_TurmaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTurma",
                table: "SubjectTurma",
                columns: new[] { "SubjectId", "TurmaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurma_Subjects_SubjectId",
                table: "SubjectTurma",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTurma_Turma_TurmaId",
                table: "SubjectTurma",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id");
        }
    }
}
