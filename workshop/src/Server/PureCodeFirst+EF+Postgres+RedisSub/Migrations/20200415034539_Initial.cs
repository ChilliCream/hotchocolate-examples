using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SlackClone.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedByEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    RecipientId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    SentAtUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Online = table.Column<bool>(nullable: false),
                    LastSeen = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "ChannelMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CreatedAtUTC = table.Column<DateTime>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    ThreadId = table.Column<Guid>(nullable: true),
                    ChannelId = table.Column<Guid>(nullable: false),
                    CreatedByEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMessages_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelMembers",
                columns: table => new
                {
                    ChannelId = table.Column<Guid>(nullable: false),
                    MemberEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelMembers", x => new { x.ChannelId, x.MemberEmail });
                    table.ForeignKey(
                        name: "FK_ChannelMembers_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelMembers_Users_MemberEmail",
                        column: x => x.MemberEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMembers_MemberEmail",
                table: "ChannelMembers",
                column: "MemberEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMessages_ChannelId",
                table: "ChannelMessages",
                column: "ChannelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelMembers");

            migrationBuilder.DropTable(
                name: "ChannelMessages");

            migrationBuilder.DropTable(
                name: "DirectMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Channels");
        }
    }
}
