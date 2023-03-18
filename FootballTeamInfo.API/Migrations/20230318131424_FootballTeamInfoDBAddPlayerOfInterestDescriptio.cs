using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamInfo.API.Migrations
{
    public partial class FootballTeamInfoDBAddPlayerOfInterestDescriptio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PlayerOfInterests",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PlayerOfInterests");
        }
    }
}
