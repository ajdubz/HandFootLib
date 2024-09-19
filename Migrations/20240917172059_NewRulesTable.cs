using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandFootLib.Migrations
{
    /// <inheritdoc />
    public partial class NewRulesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "GameTeams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TotalScore",
                table: "GameRounds",
                newName: "RedThrees");

            migrationBuilder.AddColumn<int>(
                name: "CardsToDraw",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardsToStart",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CleanBookScore",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirtyBookScore",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PulledScore",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RedThreeScore",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoundThresholds",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerScore",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CleanBooks",
                table: "GameRounds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirtyBooks",
                table: "GameRounds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HandScore",
                table: "GameRounds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWinner",
                table: "GameRounds",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PulledCorrect",
                table: "GameRounds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanBookScore = table.Column<int>(type: "int", nullable: true),
                    DirtyBookScore = table.Column<int>(type: "int", nullable: true),
                    RedThreeScore = table.Column<int>(type: "int", nullable: true),
                    PulledScore = table.Column<int>(type: "int", nullable: true),
                    WinnerScore = table.Column<int>(type: "int", nullable: true),
                    CardsToDraw = table.Column<int>(type: "int", nullable: true),
                    CardsToStart = table.Column<int>(type: "int", nullable: true),
                    RoundThresholds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropColumn(
                name: "CardsToDraw",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CardsToStart",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CleanBookScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DirtyBookScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PulledScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RedThreeScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RoundThresholds",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WinnerScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CleanBooks",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "DirtyBooks",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "HandScore",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "IsWinner",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "PulledCorrect",
                table: "GameRounds");

            migrationBuilder.RenameColumn(
                name: "RedThrees",
                table: "GameRounds",
                newName: "TotalScore");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "GameTeams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Games",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
