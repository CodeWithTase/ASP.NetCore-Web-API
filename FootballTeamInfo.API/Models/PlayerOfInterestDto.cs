namespace FootballTeamInfo.API.Models
{
    public class PlayerOfInterestDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
