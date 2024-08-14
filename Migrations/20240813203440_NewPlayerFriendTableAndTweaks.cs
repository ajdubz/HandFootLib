using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandFootLib.Migrations
{
    /// <inheritdoc />
    public partial class NewPlayerFriendTableAndTweaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendIds",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "PlayerFriends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    FriendId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFriends", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerFriends");

            migrationBuilder.AddColumn<string>(
                name: "FriendIds",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
