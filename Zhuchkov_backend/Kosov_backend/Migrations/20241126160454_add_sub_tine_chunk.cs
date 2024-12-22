using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kosov_backend.Migrations
{
    public partial class add_sub_tine_chunk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateTelegram");

            migrationBuilder.DropColumn(
                name: "IdStateTelegram",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdTimeChunks",
                table: "SubscribeRoom");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTimeChunk");

            migrationBuilder.AddColumn<int>(
                name: "IdStateTelegram",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTimeChunks",
                table: "SubscribeRoom",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StateTelegram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateTelegram", x => x.Id);
                });
        }
    }
}
