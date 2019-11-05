using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class addLikeToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownvotepostData",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpvotepostData",
                table: "Profiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownvotepostData",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UpvotepostData",
                table: "Profiles");
        }
    }
}
