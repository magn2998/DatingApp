using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class ProfilesPostsCommentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfilesId",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfilesId1",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    ProfileName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false),
                    ProfileDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostId = table.Column<int>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommentId = table.Column<int>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProfilesId",
                table: "Messages",
                column: "ProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProfilesId1",
                table: "Messages",
                column: "ProfilesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProfileId",
                table: "Comments",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProfileId",
                table: "Posts",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Profiles_ProfilesId",
                table: "Messages",
                column: "ProfilesId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Profiles_ProfilesId1",
                table: "Messages",
                column: "ProfilesId1",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Profiles_ProfilesId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Profiles_ProfilesId1",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ProfilesId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ProfilesId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ProfilesId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ProfilesId1",
                table: "Messages");
        }
    }
}
