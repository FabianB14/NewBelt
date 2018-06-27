using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NewBelt.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "newusers",
                columns: table => new
                {
                    UsersId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Alias = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_newusers", x => x.UsersId);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AliasName = table.Column<string>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    Posts = table.Column<string>(nullable: false),
                    UsersId = table.Column<int>(nullable: false),
                    numParticipants = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_post_newusers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "newusers",
                        principalColumn: "UsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "likers",
                columns: table => new
                {
                    LikersId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PostId = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UsersId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likers", x => x.LikersId);
                    table.ForeignKey(
                        name: "FK_likers_post_PostId",
                        column: x => x.PostId,
                        principalTable: "post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likers_newusers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "newusers",
                        principalColumn: "UsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_likers_PostId",
                table: "likers",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_likers_UsersId",
                table: "likers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_post_UsersId",
                table: "post",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "likers");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "newusers");
        }
    }
}
