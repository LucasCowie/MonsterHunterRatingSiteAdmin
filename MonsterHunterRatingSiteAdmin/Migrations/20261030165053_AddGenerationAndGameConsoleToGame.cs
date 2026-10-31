using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonsterHunterRatingSiteAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddGenerationAndGameConsoleToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Console",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Game",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GameIcon",
                table: "Game",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Game",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GameConsoleId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenerationId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameConsole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameConsole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameConsoleId",
                table: "Game",
                column: "GameConsoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GenerationId",
                table: "Game",
                column: "GenerationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_GameConsole_GameConsoleId",
                table: "Game",
                column: "GameConsoleId",
                principalTable: "GameConsole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Generation_GenerationId",
                table: "Game",
                column: "GenerationId",
                principalTable: "Generation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_GameConsole_GameConsoleId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Generation_GenerationId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "GameConsole");

            migrationBuilder.DropTable(
                name: "Generation");

            migrationBuilder.DropIndex(
                name: "IX_Game_GameConsoleId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GenerationId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GameConsoleId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "GameIcon",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "Console",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Generation",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
