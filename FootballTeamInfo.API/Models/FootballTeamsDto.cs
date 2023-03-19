namespace FootballTeamInfo.API.Models
{
    public class FootballTeamsDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int NumberOfPlayerOfInterests
        {
            get
            {
                return PlayersOfInterest.Count();
            }
        }

        public ICollection<PlayerOfInterestDto> PlayersOfInterest { get; set; } = new List<PlayerOfInterestDto>();
    }
}
