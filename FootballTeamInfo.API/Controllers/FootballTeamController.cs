using AutoMapper;
using FootballTeamInfo.API.Models;
using FootballTeamInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FootballTeamInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/footballteams")]
    public class FootballTeamController : ControllerBase
    {
        private readonly IFootballTeamInfoRepository _footballTeamInfoRepository;
        private readonly IMapper _mapper;
        const int maxFootballTeamPageSize = 20;

        public FootballTeamController(IFootballTeamInfoRepository footballTeamInfoRepository,
            IMapper mapper)
        {
            _footballTeamInfoRepository = footballTeamInfoRepository ?? 
                throw new ArgumentNullException(nameof(footballTeamInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FootballTeamsWithoutPlayersOfInterestDto>>> GetFootballTeams(
           [FromQuery] string? name, [FromQuery] string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxFootballTeamPageSize)
            {
                pageSize = maxFootballTeamPageSize;
            }
            var (footbalTeams, paginationMetadata) = await _footballTeamInfoRepository.GetFootballTeamsAsync(
                name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

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
