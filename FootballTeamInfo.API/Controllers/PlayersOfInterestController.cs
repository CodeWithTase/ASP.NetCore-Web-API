using AutoMapper;
using FootballTeamInfo.API.Entities;
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
        private readonly IFootballTeamInfoRepository _footballTeamInfoRepository;
        private readonly IMapper _mapper;

        public PlayersOfInterestController(ILogger<PlayersOfInterestController> logger, 
            IMailService mailService, 
            IFootballTeamInfoRepository footballTeamInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? 
                throw new ArgumentNullException(nameof(mailService));
            _footballTeamInfoRepository = footballTeamInfoRepository ?? 
                throw new ArgumentNullException(nameof(footballTeamInfoRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerOfInterestDto>>> GetPlayersOfInterest(int footballTeamId)
        {
            if (!await _footballTeamInfoRepository.FootballTeamExistsAsync(footballTeamId))
            {
                _logger.LogInformation($"The football team with Id {footballTeamId} was not found");
                return NotFound();
            }
            var playersOfinterestForFootballTeam = await _footballTeamInfoRepository.GetPlayersOfInterestAsync(footballTeamId);

            return Ok(_mapper.Map<IEnumerable<PlayerOfInterestDto>> (playersOfinterestForFootballTeam));
        }

        [HttpGet("{playerOfInterestId}", Name = "GetPlayerOfInterest")]
        public async Task<ActionResult<PlayerOfInterestDto>> GetPlayerOfInterest(int footballTeamId, int playerOfInterestId)
        {
            try
            {
                //testing Serilog
                //throw new Exception("An exception");

                if (!await _footballTeamInfoRepository.FootballTeamExistsAsync(footballTeamId))
                {
                    _logger.LogInformation($"Football team with ID: {footballTeamId} was not found when trying to access players of interest.");
                    return NotFound();
                }

                var playerOfInterest = await _footballTeamInfoRepository.GetPlayerOfInterestAsync(footballTeamId, playerOfInterestId);

                if (playerOfInterest == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<PlayerOfInterestDto>(playerOfInterest));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An exception occured whilst trying to get player of interest with ID {playerOfInterestId}", ex);
                return StatusCode(500, "A problem happened whilst handling your request");
            }

        }

        [HttpPost]
        public async Task<ActionResult<PlayerOfInterestDto>> CreatePlayerOfInterest(int footballTeamId, PlayerOfInterestCreationDto playerOfInterest)
        {
            if (!await _footballTeamInfoRepository.FootballTeamExistsAsync(footballTeamId))
            {
                return NotFound();
            }

            var finalPlayerOfInterest = _mapper.Map<PlayerOfInterest>(playerOfInterest);

            await _footballTeamInfoRepository.AddPlayerOfInterestForFootballTeamAsync(footballTeamId, finalPlayerOfInterest);

            await _footballTeamInfoRepository.SaveChangesAsync();

            var createdPlayerOfinterest = _mapper.Map<PlayerOfInterestDto>(finalPlayerOfInterest);
 
            return CreatedAtRoute("GetPlayerOfInterest",
                new
                {
                    footballTeamId = footballTeamId,
                    playerOfInterestId = createdPlayerOfinterest.ID
                },
                createdPlayerOfinterest);
        }

        [HttpPut("{playerOfInterestId}")]
        public async Task<ActionResult> UpdatePlayerOfInterest(int footballTeamId, int playerOfInterestId, PlayerOfInterestUpdateDto playerOfInterest)
        {
            if (!await _footballTeamInfoRepository.FootballTeamExistsAsync(footballTeamId))
            {
                return NotFound();
            }

            var playerOfinterestEntity = await _footballTeamInfoRepository
                .GetPlayerOfInterestAsync(footballTeamId, playerOfInterestId);

            if (playerOfinterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(playerOfInterest, playerOfinterestEntity);

            await _footballTeamInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{playerOfInterestId}")]
        public async Task<ActionResult> PartiallyUpdatePlayerOfInterest(
            int footballTeamId, int playerOfInterestId,
            JsonPatchDocument<PlayerOfInterestUpdateDto> patchDocument)
        {
            if (!await _footballTeamInfoRepository.FootballTeamExistsAsync(footballTeamId))
            {
                return NotFound();
            }

            var playerOfinterestEntity = await _footballTeamInfoRepository
                .GetPlayerOfInterestAsync(footballTeamId, playerOfInterestId);

            if (playerOfinterestEntity == null) return NotFound();

            var playerToPatch = _mapper.Map<PlayerOfInterestUpdateDto>(playerOfinterestEntity);

            patchDocument.ApplyTo(playerToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(playerToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(playerToPatch, playerOfinterestEntity);

            await _footballTeamInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{PlayerOfInterestId}")]
        public async Task<ActionResult> DeletePlayerOfInterest(int footballTeamId, int playerOfInterestId)
        {
            if (!await _footballTeamInfoRepository.FootballTeamExistsAsync(footballTeamId))
            {
                return NotFound();
            }

            var playerOfinterestEntity = await _footballTeamInfoRepository
                .GetPlayerOfInterestAsync(footballTeamId, playerOfInterestId);

            if (playerOfinterestEntity == null) return NotFound();

            _footballTeamInfoRepository.DeletPlayerOfInterest(playerOfinterestEntity);

            await _footballTeamInfoRepository.SaveChangesAsync();

            _mailService.Send(
                "Player of interest was deleted",
                $"Player of interest {playerOfinterestEntity.Name} with id {playerOfinterestEntity.Id} was deleted.");

            return NoContent();
        }
    }
}
