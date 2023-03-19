using AutoMapper;

namespace FootballTeamInfo.API.Profiles
{
    public class FootballTeamProfile : Profile
    {
        public FootballTeamProfile()
        {
            CreateMap<Entities.FootballTeam, Models.FootballTeamsWithoutPlayersOfInterestDto>();
            CreateMap<Entities.FootballTeam, Models.FootballTeamsDto>();
        }
    }
}
