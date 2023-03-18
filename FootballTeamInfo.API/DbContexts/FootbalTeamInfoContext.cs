using FootballTeamInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballTeamInfo.API.DbContexts
{
    public class FootbalTeamInfoContext : DbContext
    {
        public DbSet<FootballTeam> FootballTeams { get; set; } = null!;
        public DbSet<PlayerOfInterest> PlayerOfInterests { get; set; } = null!;

        public FootbalTeamInfoContext(DbContextOptions<FootbalTeamInfoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FootballTeam>()
                .HasData(
                 new FootballTeam("Arsenal")
                 {
                     Id = 1,
                     Description = "Arsenal Football Club is an English professional football " +
                     "club based in Islington, London. Arsenal plays in the Premier League, the top flight of English football.",
                 },
                new FootballTeam("Barcelona")
                {
                    Id = 2,
                    Name = "Barcelona",
                    Description = "Futbol Club Barcelona, commonly referred to as Barcelona " +
                    "and colloquially known as Barça, is a professional football club based in Barcelona, " +
                    "Catalonia, Spain, that competes in La Liga, the top flight of Spanish football.",
                },
                new FootballTeam("Real Madrid")
                {
                    Id = 3,
                    Description = "Real Madrid Club de Fútbol, commonly referred to as Real Madrid, is a " +
                    "Spanish professional football club based in Madrid. Founded in 1902 as Madrid Football Club, " +
                    "the club has traditionally worn a white home kit since its inception.",
                },
                new FootballTeam("Liverpool")
                {
                    Id = 4,
                    Description =
                    "Liverpool Football Club is a professional football club based in Liverpool, England. " +
                    "The club competes in the Premier League, the top tier of English football. Founded in " +
                    "1892, the club joined the Football League the following year and has played its home " +
                    "games at Anfield since its formation.",
                });

            modelBuilder.Entity<PlayerOfInterest>()
                .HasData(
                new PlayerOfInterest("Thierry Henry")
                {
                    Id=1,
                    Description = "Record goal Scorer",
                    FootballTeamId = 1
                },
                new PlayerOfInterest("Lionel Messi")
                {
                    Id = 2,
                    Description = "Best football player ever",
                    FootballTeamId = 2
                },
                new PlayerOfInterest("Cristiano Ronaldo")
                {
                    Id = 3,
                    Description = "Best player in club history",
                    FootballTeamId = 3
                },
                new PlayerOfInterest("Steven Gerrard")
                {
                    Id = 4,
                    Description = "One of the best players in club history",
                    FootballTeamId = 4
                });

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
