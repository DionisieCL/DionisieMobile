using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class RemoveIdFromStudentLessonData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsDatas_LessonDatas_LessonDataId",
                table: "StudentLessonsDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonsDatas_Students_StudentId",
                table: "StudentLessonsDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLessonsDatas",
                table: "StudentLessonsDatas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentLessonsDatas");

            migrationBuilder.RenameTable(
                name: "StudentLessonsDatas",
                newName: "StudentLessonDatas");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessonsDatas_LessonDataId",
                table: "StudentLessonDatas",
                newName: "IX_StudentLessonDatas_LessonDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLessonDatas",
                table: "StudentLessonDatas",
                columns: new[] { "StudentId", "LessonDataId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonDatas_LessonDatas_LessonDataId",
                table: "StudentLessonDatas",
                column: "LessonDataId",
                principalTable: "LessonDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessonDatas_Students_StudentId",
                table: "StudentLessonDatas",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonDatas_LessonDatas_LessonDataId",
                table: "StudentLessonDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessonDatas_Students_StudentId",
                table: "StudentLessonDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLessonDatas",
                table: "StudentLessonDatas");

            migrationBuilder.RenameTable(
                name: "StudentLessonDatas",
                newName: "StudentLessonsDatas");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessonDatas_LessonDataId",
                table: "StudentLessonsDatas",
                newName: "IX_StudentLessonsDatas_LessonDataId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentLessonsDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLessonsDatas",
                table: "StudentLessonsDatas",
                columns: new[] { "StudentId", "LessonDataId" });

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
        }
    }
}
