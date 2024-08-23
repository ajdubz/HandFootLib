using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandFootLib.Migrations
{
    /// <inheritdoc />
    public partial class CoupleTweaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRound_GameTeams_GameTeamId",
                table: "GameRound");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameRound",
                table: "GameRound");

            migrationBuilder.RenameTable(
                name: "GameRound",
                newName: "GameRounds");

            migrationBuilder.RenameIndex(
                name: "IX_GameRound_GameTeamId",
                table: "GameRounds",
                newName: "IX_GameRounds_GameTeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameRounds",
                table: "GameRounds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_GameTeams_GameTeamId",
                table: "GameRounds",
                column: "GameTeamId",
                principalTable: "GameTeams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_GameTeams_GameTeamId",
                table: "GameRounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameRounds",
                table: "GameRounds");

            migrationBuilder.RenameTable(
                name: "GameRounds",
                newName: "GameRound");

            migrationBuilder.RenameIndex(
                name: "IX_GameRounds_GameTeamId",
                table: "GameRound",
                newName: "IX_GameRound_GameTeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameRound",
                table: "GameRound",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRound_GameTeams_GameTeamId",
                table: "GameRound",
                column: "GameTeamId",
                principalTable: "GameTeams",
                principalColumn: "Id");
        }
    }
}
