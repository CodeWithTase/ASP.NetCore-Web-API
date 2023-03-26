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
        void DeletPlayerOfInterest(PlayerOfInterest playerOfInterest);
        Task<(IEnumerable<FootballTeam>, PaginationMetadata)> GetFootballTeamsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<bool> FotballTeamMatchesFootballTeamId(string? footballTeam, int footballteamId);
        Task<bool> SaveChangesAsync();
    }
}
