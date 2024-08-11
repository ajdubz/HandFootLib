using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandFootLib.Migrations
{
    /// <inheritdoc />
    public partial class foreignKeyChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_winnerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_winnerId",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Games_winnerId",
                table: "Games",
                column: "winnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_winnerId",
                table: "Games",
                column: "winnerId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
