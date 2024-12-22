using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kosov_backend.Migrations
{
    public partial class add_many_to_many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTimeChunk");

            migrationBuilder.CreateTable(
                name: "SubscribeRoomTimeChunk",
                columns: table => new
                {
                    SubscribeRoomId = table.Column<int>(type: "int", nullable: false),
                    TimeChunkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribeRoomTimeChunk", x => new { x.SubscribeRoomId, x.TimeChunkId });
                    table.ForeignKey(
                        name: "FK_SubscribeRoomTimeChunk_SubscribeRoom_SubscribeRoomId",
                        column: x => x.SubscribeRoomId,
                        principalTable: "SubscribeRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscribeRoomTimeChunk_TimeChunk_TimeChunkId",
                        column: x => x.TimeChunkId,
                        principalTable: "TimeChunk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscribeRoomTimeChunk_TimeChunkId",
                table: "SubscribeRoomTimeChunk",
                column: "TimeChunkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscribeRoomTimeChunk");

            migrationBuilder.CreateTable(
                name: "SubTimeChunk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSub = table.Column<int>(type: "int", nullable: false),
                    IdTimeChunk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTimeChunk", x => x.Id);
                });
        }
    }
}
