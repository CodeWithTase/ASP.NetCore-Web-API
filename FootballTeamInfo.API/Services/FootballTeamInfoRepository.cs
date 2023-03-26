using FootballTeamInfo.API.DbContexts;
using FootballTeamInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballTeamInfo.API.Services
{
    public class FootballTeamInfoRepository : IFootballTeamInfoRepository
    {
        private readonly FootbalTeamInfoContext _context;

        public FootballTeamInfoRepository(FootbalTeamInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<FootballTeam>> GetFootballTeamsAsync()
        {
            return await _context.FootballTeams.OrderBy(f => f.Name).ToListAsync();
        }

        public async Task<(IEnumerable<FootballTeam>, PaginationMetadata)> GetFootballTeamsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.FootballTeams as IQueryable<FootballTeam>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(f => f.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                || (a.Description !=null && a.Description.Contains(searchQuery)));   
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn =  await collection.OrderBy(f => f.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<FootballTeam?> GetFootballTeamAsync(int footballTeamId, bool includePlayersOfInterest)
        {
            if (includePlayersOfInterest)
            {
                return await _context.FootballTeams.Include(f => f.PlayersOfInterest)
                    .Where(f => f.Id == footballTeamId).FirstOrDefaultAsync();
            }

            return await _context.FootballTeams.Where(f => f.Id == footballTeamId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlayerOfInterest>> GetPlayersOfInterestAsync(int footballTeamId)
        {
            return await _context.PlayerOfInterests.Where(p => p.Id == footballTeamId).ToListAsync();
        }

        public async Task<PlayerOfInterest?> GetPlayerOfInterestAsync(int footballTeamId, int playerOfInterestId)
        {
            return await _context.PlayerOfInterests
                .Where(p => p.FootballTeamId == footballTeamId && p.Id == playerOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<bool> FootballTeamExistsAsync(int footballTeamId)
        {
            return await _context.FootballTeams.AnyAsync(f => f.Id == footballTeamId);
        }

        public async Task<bool> FotballTeamMatchesFootballTeamId(string? footballTeam, int footballteamId)
        {
            return await _context.FootballTeams.AnyAsync(f => f.Id == footballteamId && f.Name == footballTeam);
        }

        public async Task AddPlayerOfInterestForFootballTeamAsync(int footballTeamId, PlayerOfInterest playerOfInterest) 
        {
            var footballTeam = await GetFootballTeamAsync(footballTeamId, false);
            if (footballTeam != null)
            {
                footballTeam.PlayersOfInterest.Add(playerOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeletPlayerOfInterest(PlayerOfInterest playerOfInterest)
        {
            _context.PlayerOfInterests.Remove(playerOfInterest);
        }
    }
}
