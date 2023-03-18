using FootballTeamInfo.API.Models;

namespace FootballTeamInfo.API
{
    public class FootballTeamDataStore
    {
        public List<FootballTeamsDto> FootballTeams { get; set; }

        public FootballTeamDataStore()
        {
            FootballTeams = new List<FootballTeamsDto>()
            {
                new FootballTeamsDto()
                {
                    ID = 1,
                    Name = "Arsenal",
                    Description = "Arsenal Football Club is an English professional football club based in Islington, London. Arsenal plays in the Premier League, the top flight of English football.",
                    PlayersOfInterests = new List<PlayerOfInterestDto>()
                    {
                      new PlayerOfInterestDto()
                      {
                          ID= 21,
                          Name = "Thierry Henry",
                          Description="Record goal Scorer"
                      }
                    }

                } ,
                new FootballTeamsDto()
                {
                    ID = 2,
                    Name ="Barcelona",
                    Description = "Futbol Club Barcelona, commonly referred to as Barcelona and colloquially known as Barça, is a professional football club based in Barcelona, Catalonia, Spain, that competes in La Liga, the top flight of Spanish football.",
                    PlayersOfInterests = new List<PlayerOfInterestDto>()
                    {
                      new PlayerOfInterestDto()
                      {
                          ID= 22,
                          Name = "Lionel Messi",
                          Description="Best player in club history"
                      }
                    }
                },
                new FootballTeamsDto()
                {
                    ID= 3,
                    Name = "Real Madrid",
                    Description = "Real Madrid Club de Fútbol, commonly referred to as Real Madrid, is a Spanish professional football club based in Madrid. Founded in 1902 as Madrid Football Club, the club has traditionally worn a white home kit since its inception.",
                    PlayersOfInterests = new List<PlayerOfInterestDto>()
                    {
                      new PlayerOfInterestDto()
                      {
                          ID= 23,
                          Name = "Cristiano Ronaldo",
                          Description="Best player in club history"
                      }
                    }
                },
                new FootballTeamsDto()
                {
                    ID= 4,
                    Name = "Liverpool",
                    Description =
                    "Liverpool Football Club is a professional football club based in Liverpool, England. The club competes in the Premier League, the top tier of English football. Founded in 1892, the club joined the Football League the following year and has played its home games at Anfield since its formation.",
                    PlayersOfInterests = new List<PlayerOfInterestDto>()
                    {
                      new PlayerOfInterestDto()
                      {
                          ID= 24,
                          Name = "Steven Gerrard",
                          Description="One of the best players in club history"
                      }
                    }
                }
            };
        }
    }
}
