using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandFootLib.Migrations
{
    /// <inheritdoc />
    public partial class NewForeignTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_GameId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Teams");

            migrationBuilder.CreateTable(
                name: "GameTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTeams_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameTeams_GameId",
                table: "GameTeams",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTeams_TeamId",
                table: "GameTeams",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTeams");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
