using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class AlterRoomLessonRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_RoomId",
                table: "Lessons");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_RoomId",
                table: "Lessons",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_RoomId",
                table: "Lessons");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_RoomId",
                table: "Lessons",
                column: "RoomId",
                unique: true);
        }
    }
}
