using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class SubjectTurma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Turma_TurmaId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TurmaId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "SubjectTurma",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTurma", x => new { x.SubjectId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_SubjectTurma_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectTurma_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTurma_TurmaId",
                table: "SubjectTurma",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTurma");

            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TurmaId",
                table: "Subjects",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Turma_TurmaId",
                table: "Subjects",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
