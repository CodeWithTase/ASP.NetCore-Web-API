using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamInfo.API.Migrations
{
    public partial class DataSeedAndUpdateToPlayerOfInterestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOfInterests_FootballTeams_CityId",
                table: "PlayerOfInterests");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "PlayerOfInterests",
                newName: "FootballTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerOfInterests_CityId",
                table: "PlayerOfInterests",
                newName: "IX_PlayerOfInterests_FootballTeamId");

            migrationBuilder.InsertData(
                table: "FootballTeams",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Arsenal Football Club is an English professional football club based in Islington, London. Arsenal plays in the Premier League, the top flight of English football.", "Arsenal" });

            migrationBuilder.InsertData(
                table: "FootballTeams",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Futbol Club Barcelona, commonly referred to as Barcelona and colloquially known as Barça, is a professional football club based in Barcelona, Catalonia, Spain, that competes in La Liga, the top flight of Spanish football.", "Barcelona" });

            migrationBuilder.InsertData(
                table: "FootballTeams",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Real Madrid Club de Fútbol, commonly referred to as Real Madrid, is a Spanish professional football club based in Madrid. Founded in 1902 as Madrid Football Club, the club has traditionally worn a white home kit since its inception.", "Real Madrid" });

            migrationBuilder.InsertData(
                table: "FootballTeams",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "Liverpool Football Club is a professional football club based in Liverpool, England. The club competes in the Premier League, the top tier of English football. Founded in 1892, the club joined the Football League the following year and has played its home games at Anfield since its formation.", "Liverpool" });

            migrationBuilder.InsertData(
                table: "PlayerOfInterests",
                columns: new[] { "Id", "Description", "FootballTeamId", "Name" },
                values: new object[] { 1, "Record goal Scorer", 1, "Thierry Henry" });

            migrationBuilder.InsertData(
                table: "PlayerOfInterests",
                columns: new[] { "Id", "Description", "FootballTeamId", "Name" },
                values: new object[] { 2, "Best football player ever", 2, "Lionel Messi" });

            migrationBuilder.InsertData(
                table: "PlayerOfInterests",
                columns: new[] { "Id", "Description", "FootballTeamId", "Name" },
                values: new object[] { 3, "Best player in club history", 3, "Cristiano Ronaldo" });

            migrationBuilder.InsertData(
                table: "PlayerOfInterests",
                columns: new[] { "Id", "Description", "FootballTeamId", "Name" },
                values: new object[] { 4, "One of the best players in club history", 4, "Steven Gerrard" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOfInterests_FootballTeams_FootballTeamId",
                table: "PlayerOfInterests",
                column: "FootballTeamId",
                principalTable: "FootballTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOfInterests_FootballTeams_FootballTeamId",
                table: "PlayerOfInterests");

            migrationBuilder.DeleteData(
                table: "PlayerOfInterests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlayerOfInterests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlayerOfInterests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlayerOfInterests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FootballTeams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FootballTeams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FootballTeams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FootballTeams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "FootballTeamId",
                table: "PlayerOfInterests",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerOfInterests_FootballTeamId",
                table: "PlayerOfInterests",
                newName: "IX_PlayerOfInterests_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOfInterests_FootballTeams_CityId",
                table: "PlayerOfInterests",
                column: "CityId",
                principalTable: "FootballTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
