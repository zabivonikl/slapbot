using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Punishment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ChatId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Gameid = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Users_Games_Gameid",
                        column: x => x.Gameid,
                        principalTable: "Games",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChatId = table.Column<long>(type: "INTEGER", nullable: false),
                    Gameid = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invitations_Games_Gameid",
                        column: x => x.Gameid,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Users",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slap",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gameid = table.Column<long>(type: "INTEGER", nullable: false),
                    FromChatId = table.Column<long>(type: "INTEGER", nullable: false),
                    ToChatId = table.Column<long>(type: "INTEGER", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slap", x => x.id);
                    table.ForeignKey(
                        name: "FK_Slap_Games_Gameid",
                        column: x => x.Gameid,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slap_Users_FromChatId",
                        column: x => x.FromChatId,
                        principalTable: "Users",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slap_Users_ToChatId",
                        column: x => x.ToChatId,
                        principalTable: "Users",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ChatId",
                table: "Invitations",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Gameid",
                table: "Invitations",
                column: "Gameid");

            migrationBuilder.CreateIndex(
                name: "IX_Slap_FromChatId",
                table: "Slap",
                column: "FromChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Slap_Gameid",
                table: "Slap",
                column: "Gameid");

            migrationBuilder.CreateIndex(
                name: "IX_Slap_ToChatId",
                table: "Slap",
                column: "ToChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Gameid",
                table: "Users",
                column: "Gameid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Slap");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
