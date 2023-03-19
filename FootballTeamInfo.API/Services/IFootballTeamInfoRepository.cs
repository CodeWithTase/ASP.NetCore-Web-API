using FootballTeamInfo.API.Entities;

namespace FootballTeamInfo.API.Services
{
    public interface IFootballTeamInfoRepository
    {
        Task<IEnumerable<FootballTeam>> GetFootballTeamsAsync();
        Task<FootballTeam?> GetFootballTeamAsync(int footballTeamId, bool includePlayersOfInterest);
        Task<IEnumerable<PlayerOfInterest>> GetPlayersOfInterestAsync(int footballTeamId);
        Task<PlayerOfInterest?> GetPlayerOfInterestAsync(int footballTeamId, int playerOfInterestId);
        Task<bool> FootballTeamExistsAsync(int footballTeamId);
        Task AddPlayerOfInterestForFootballTeamAsync(int footballTeamId, PlayerOfInterest playerOfInterest);
        Task<bool> SaveChangesAsync();
        void DeletPlayerOfInterest(PlayerOfInterest playerOfInterest);
    }
}
