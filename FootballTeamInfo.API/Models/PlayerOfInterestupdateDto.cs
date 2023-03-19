using System.ComponentModel.DataAnnotations;

namespace FootballTeamInfo.API.Models
{
    public class PlayerOfInterestUpdateDto
    {
        [Required(ErrorMessage = "Player name should be provided")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
