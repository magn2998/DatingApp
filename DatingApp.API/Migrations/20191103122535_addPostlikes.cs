using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class addPostlikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownvotepostData",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UpvotepostData",
                table: "Profiles");

            migrationBuilder.CreateTable(
                name: "Postlike",
                columns: table => new
                {
                    LikerId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    Reaction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postlike", x => new { x.LikerId, x.PostId });
                    table.ForeignKey(
                        name: "FK_Postlike_Profiles_LikerId",
                        column: x => x.LikerId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postlike_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postlike_PostId",
                table: "Postlike",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postlike");

            migrationBuilder.AddColumn<string>(
                name: "DownvotepostData",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpvotepostData",
                table: "Profiles",
                nullable: true);
        }
    }
}
