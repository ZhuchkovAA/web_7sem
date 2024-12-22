using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kosov_backend.Migrations
{
    public partial class visual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscribeRoomTimeChunk_SubscribeRoom_SubscribeRoomId",
                table: "SubscribeRoomTimeChunk");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscribeRoomTimeChunk_TimeChunk_TimeChunkId",
                table: "SubscribeRoomTimeChunk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeChunk",
                table: "TimeChunk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscribeRoom",
                table: "SubscribeRoom");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TimeChunk",
                newName: "TimeChunks");

            migrationBuilder.RenameTable(
                name: "SubscribeRoom",
                newName: "SubscribeRooms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "IdTelegram");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeChunks",
                table: "TimeChunks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscribeRooms",
                table: "SubscribeRooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscribeRoomTimeChunk_SubscribeRooms_SubscribeRoomId",
                table: "SubscribeRoomTimeChunk",
                column: "SubscribeRoomId",
                principalTable: "SubscribeRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscribeRoomTimeChunk_TimeChunks_TimeChunkId",
                table: "SubscribeRoomTimeChunk",
                column: "TimeChunkId",
                principalTable: "TimeChunks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscribeRoomTimeChunk_SubscribeRooms_SubscribeRoomId",
                table: "SubscribeRoomTimeChunk");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscribeRoomTimeChunk_TimeChunks_TimeChunkId",
                table: "SubscribeRoomTimeChunk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeChunks",
                table: "TimeChunks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscribeRooms",
                table: "SubscribeRooms");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "TimeChunks",
                newName: "TimeChunk");

            migrationBuilder.RenameTable(
                name: "SubscribeRooms",
                newName: "SubscribeRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "IdTelegram");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeChunk",
                table: "TimeChunk",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscribeRoom",
                table: "SubscribeRoom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscribeRoomTimeChunk_SubscribeRoom_SubscribeRoomId",
                table: "SubscribeRoomTimeChunk",
                column: "SubscribeRoomId",
                principalTable: "SubscribeRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscribeRoomTimeChunk_TimeChunk_TimeChunkId",
                table: "SubscribeRoomTimeChunk",
                column: "TimeChunkId",
                principalTable: "TimeChunk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
