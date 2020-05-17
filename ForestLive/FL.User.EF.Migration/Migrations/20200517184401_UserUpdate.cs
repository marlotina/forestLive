using Microsoft.EntityFrameworkCore.Migrations;

namespace FL.User.EF.Migration.Migrations
{
    public partial class UserUpdate : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedlinUrl",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LinkedlinUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "User");
        }
    }
}
