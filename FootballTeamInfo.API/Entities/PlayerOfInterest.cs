using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballTeamInfo.API.Entities
{
    public class PlayerOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("FootballTeamId")]
        public FootballTeam? FootballTeam { get; set; }
        public int FootballTeamId { get; set; }

        public PlayerOfInterest(string name)
        {
            Name = name;
        }
    }
}
