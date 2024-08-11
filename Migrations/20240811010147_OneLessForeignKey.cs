using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandFootLib.Migrations
{
    /// <inheritdoc />
    public partial class OneLessForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "winnerId",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "IsWinner",
                table: "GameTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWinner",
                table: "GameTeams");

            migrationBuilder.AddColumn<int>(
                name: "winnerId",
                table: "Games",
                type: "int",
                nullable: true);
        }
    }
}
