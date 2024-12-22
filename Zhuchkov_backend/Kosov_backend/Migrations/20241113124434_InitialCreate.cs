using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kosov_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "SubscribeRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTelegram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRoom = table.Column<int>(type: "int", nullable: false),
                    IdTimeChunks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribeRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeChunk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeChunk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdTelegram = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagTelegram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdStateTelegram = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdTelegram);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateTelegram");

            migrationBuilder.DropTable(
                name: "SubscribeRoom");

            migrationBuilder.DropTable(
                name: "TimeChunk");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
