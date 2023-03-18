using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamInfo.API.Migrations
{
    public partial class FootballTeamInfoinitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerOfInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerOfInterests_FootballTeams_CityId",
                        column: x => x.CityId,
                        principalTable: "FootballTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOfInterests_CityId",
                table: "PlayerOfInterests",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerOfInterests");

            migrationBuilder.DropTable(
                name: "FootballTeams");
        }
    }
}
