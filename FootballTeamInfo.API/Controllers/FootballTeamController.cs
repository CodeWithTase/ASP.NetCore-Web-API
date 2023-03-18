using FootballTeamInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamInfo.API.Controllers
{
    [ApiController]
    [Route("api/footballteams")]
    public class FootballTeamController : ControllerBase
    {
        private readonly FootballTeamDataStore _footballTeamDataStore;
        public FootballTeamController(FootballTeamDataStore footballTeamDataStore)
        {
            _footballTeamDataStore = footballTeamDataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FootballTeamsDto>> GetFootballTeams()
        {
            return Ok(_footballTeamDataStore.FootballTeams);
        }

        [HttpGet("{id}")]
        public ActionResult<FootballTeamsDto> GetFootballTeam(int id)
        {
            var footballTeam = _footballTeamDataStore.FootballTeams.Where(footballTeam => footballTeam.ID == 1);

            if (footballTeam == null)
            {
                return NotFound();
            }

            return Ok(footballTeam);
        }
    }
}
