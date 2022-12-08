using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolager.Web.Migrations
{
    public partial class addRoomsToLessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_RoomId",
                table: "Lessons",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Rooms_RoomId",
                table: "Lessons",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Rooms_RoomId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_RoomId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
