using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class AddHolidays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Homework",
            //    table: "LessonDatas",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "Doubts",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StudentId = table.Column<int>(type: "int", nullable: false),
            //        LessonDataId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Doubts", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Doubts_LessonDatas_LessonDataId",
            //            column: x => x.LessonDataId,
            //            principalTable: "LessonDatas",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Doubts_Students_StudentId",
            //            column: x => x.StudentId,
            //            principalTable: "Students",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Doubts_LessonDataId",
            //    table: "Doubts",
            //    column: "LessonDataId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Doubts_StudentId",
            //    table: "Doubts",
            //    column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Doubts");

            migrationBuilder.DropTable(
                name: "Holidays");

            //migrationBuilder.DropColumn(
            //    name: "Homework",
            //    table: "LessonDatas");
        }
    }
}
