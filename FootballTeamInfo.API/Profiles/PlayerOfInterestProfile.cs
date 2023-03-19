using AutoMapper;

namespace FootballTeamInfo.API.Profiles
{
    public class PlayerOfInterestProfile : Profile
    {
        public PlayerOfInterestProfile()
        {
            CreateMap<Entities.PlayerOfInterest, Models.PlayerOfInterestDto>();
            CreateMap<Models.PlayerOfInterestCreationDto, Entities.PlayerOfInterest>();
            CreateMap<Models.PlayerOfInterestUpdateDto, Entities.PlayerOfInterest>();
            CreateMap<Entities.PlayerOfInterest, Models.PlayerOfInterestUpdateDto>();
        }
    }
}
