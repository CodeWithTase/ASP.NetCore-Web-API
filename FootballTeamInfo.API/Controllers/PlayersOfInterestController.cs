using FootballTeamInfo.API.Models;
using FootballTeamInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamInfo.API.Controllers
{
    [Route("api/footballTeam/{footballTeamId}/playersofinterest")]
    [ApiController]
    public class PlayersOfInterestController : ControllerBase
    {
        private readonly ILogger<PlayersOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly FootballTeamDataStore _footballTeamDataStore;

        public PlayersOfInterestController(ILogger<PlayersOfInterestController> logger, 
            IMailService mailService,
            FootballTeamDataStore footballTeamDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _footballTeamDataStore = footballTeamDataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlayerOfInterestDto>> GetPlayersOfInterest(int footbalTeamId)
        {
            var footballTeam = _footballTeamDataStore.FootballTeams.FirstOrDefault(footballTeams =>
            footballTeams.ID == footbalTeamId);

            if (footballTeam == null) return NotFound();

            return Ok(footballTeam.PlayersOfInterests);
        }

        [HttpGet("{playerOfInterestId}", Name = "GetPlayerOfInterest")]
        public ActionResult<PlayerOfInterestDto> GetPlayerOfInterest(int footballTeamId, int playerOfInterestId)
        {
            try
            {
                //testing Serilog
                //throw new Exception("An exception");
                var footballTeam = _footballTeamDataStore.FootballTeams.FirstOrDefault(footballTeams =>
                    footballTeams.ID == footballTeamId);

                if (footballTeam == null)
                {
                    _logger.LogInformation($"Football team with ID: {footballTeamId} was not found when trying to access players of interest.");
                    return NotFound();
                }

                var playerOfInterest = footballTeam.PlayersOfInterests.FirstOrDefault(playerOfInterest =>
                playerOfInterest.ID == playerOfInterestId);

                if (playerOfInterest == null) return NotFound();

                return Ok(playerOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured whilst trying to get player of interest with ID {playerOfInterestId}", ex);
                return StatusCode(500, "A problem happened whilst handling your request");
            }

        }

        [HttpPost]
        public ActionResult<PlayerOfInterestDto> CreatePlayerOfInterest(int footballTeamId, PlayerOfInterestCreationDto playerOfInterest)
        {
            var footballTeam = _footballTeamDataStore.FootballTeams.
                               FirstOrDefault(footballTeams => footballTeams.ID == footballTeamId);

            if (footballTeam == null) return NotFound();

            //temp training code will be improved
            var maxPointOfInterestId = _footballTeamDataStore.FootballTeams.SelectMany(
                                       footballTeams => footballTeams.PlayersOfInterests).
                                       Max(playerOfInterest => playerOfInterest.ID);

            var newPlayerOfInterest = new PlayerOfInterestDto()
            {
                ID = ++maxPointOfInterestId,
                Name = playerOfInterest.Name,
                Description = playerOfInterest.Description
            };

            footballTeam.PlayersOfInterests.Add(newPlayerOfInterest);
            return CreatedAtRoute("GetPlayerOfInterest",
                new
                {
                    footballTeamId = footballTeamId,
                    playerOfInterestId = newPlayerOfInterest.ID
                }, 
                newPlayerOfInterest);
        }

        [HttpPut("{playerOfInterestId}")]
        public ActionResult UpdatePlayerOfInterest(int footballTeamId, int playerOfInterestId, PlayerOfInterestupdateDto playerOfInterest)
        {
            var footballTeam = _footballTeamDataStore.FootballTeams.
                   FirstOrDefault(footballTeams => footballTeams.ID == footballTeamId);

            if (footballTeam == null) return NotFound();

            var player = footballTeam.PlayersOfInterests.FirstOrDefault(
                  playerOfInterest => playerOfInterest.ID == playerOfInterestId);

            if (player == null) return NotFound();

            player.Name = playerOfInterest.Name;
            player.Description = playerOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{playerOfInterestId}")]
        public ActionResult PartiallyUpdatePlayerOfInterest(
            int footballTeamId, int playerOfInterestId, 
            JsonPatchDocument<PlayerOfInterestupdateDto> patchDocument)
        {
            var footballTeam = _footballTeamDataStore.FootballTeams.
                   FirstOrDefault(footballTeams => footballTeams.ID == footballTeamId);

            if (footballTeam == null) return NotFound();

            var player = footballTeam.PlayersOfInterests.FirstOrDefault(
                  playerOfInterest => playerOfInterest.ID == playerOfInterestId);

            if (player == null) return NotFound();

            var playerToPatch = new PlayerOfInterestupdateDto { Name = player.Name, Description = player.Description };

            patchDocument.ApplyTo(playerToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(playerToPatch))
            {
                return BadRequest(ModelState);
            }

            player.Name = playerToPatch.Name;
            player.Description = playerToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{PlayerOfInterestId}")]
        public ActionResult DeletePlayerOfInterest(int footballTeamId, int playerOfInterestId)
        {
            var footballTeam = _footballTeamDataStore.FootballTeams.
                   FirstOrDefault(footballTeams => footballTeams.ID == footballTeamId);

            if (footballTeam == null) return NotFound();

            var player = footballTeam.PlayersOfInterests.FirstOrDefault(
                  playerOfInterest => playerOfInterest.ID == playerOfInterestId);

            if (player == null) return NotFound();

            footballTeam.PlayersOfInterests.Remove(player);
            _mailService.Send(
                "Player of interest was deleted",
                $"Player of interest {player.Name} with id {player.ID} was deleted.");

            return NoContent();
        }
    }
}
