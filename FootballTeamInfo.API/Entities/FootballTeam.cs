using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballTeamInfo.API.Entities
{
    public class FootballTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<PlayerOfInterest> PlayersOfInterest { get; set; } =
            new List<PlayerOfInterest>();

        public FootballTeam(string name)
        {
            Name = name;
        }

    }
}
