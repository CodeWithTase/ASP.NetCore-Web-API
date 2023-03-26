namespace FootballTeamInfo.API.Models
{
    /// <summary>
    /// A DTO for a city without points of interest
    /// </summary>
    public class FootballTeamsWithoutPlayersOfInterestDto
    {
        /// <summary>
        /// The id of the football team
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// the name of the football team
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// the description of the football team
        /// </summary>
        public string? Description { get; set; }
    }
}
