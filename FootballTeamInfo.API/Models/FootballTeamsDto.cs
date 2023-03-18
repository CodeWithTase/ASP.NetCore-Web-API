namespace FootballTeamInfo.API.Models
{
    public class FootballTeamsDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int NumberOfPlayersOfInterest
        {
            get
            {
                return PlayersOfInterests.Count();
            }
        }

        public ICollection<PlayerOfInterestDto> PlayersOfInterests { get; set; } = new List<PlayerOfInterestDto>();
    }
}
