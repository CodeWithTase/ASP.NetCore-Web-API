using AutoMapper;
using FootballTeamInfo.API.Models;
using FootballTeamInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamInfo.API.Controllers
{
    [ApiController]
    [Route("api/footballteams")]
    public class FootballTeamController : ControllerBase
    {
        private readonly IFootballTeamInfoRepository _footballTeamInfoRepository;
        private readonly IMapper _mapper;

        public FootballTeamController(IFootballTeamInfoRepository footballTeamInfoRepository,
            IMapper mapper)
        {
            _footballTeamInfoRepository = footballTeamInfoRepository ?? 
                throw new ArgumentNullException(nameof(footballTeamInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FootballTeamsWithoutPlayersOfInterestDto>>> GetFootballTeams()
        {
            var footbalTeams = await _footballTeamInfoRepository.GetFootballTeamsAsync();
            return Ok(_mapper.Map<IEnumerable<FootballTeamsWithoutPlayersOfInterestDto>>(footbalTeams));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFootballTeam(int id, bool includePlayersOfInterest = false)
        {
            var footballTeam = await _footballTeamInfoRepository.GetFootballTeamAsync(id, includePlayersOfInterest);

            if (footballTeam == null)
            {
                return NotFound();
            }

            if (includePlayersOfInterest)
            {
                return Ok(_mapper.Map<FootballTeamsDto>(footballTeam));
            }

            return Ok(_mapper.Map<FootballTeamsWithoutPlayersOfInterestDto>(footballTeam));
        }
    }
}
