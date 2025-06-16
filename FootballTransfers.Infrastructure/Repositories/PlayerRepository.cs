using Microsoft.EntityFrameworkCore;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;
using FootballTransfers.Infrastructure.Data;

namespace FootballTransfers.Infrastructure.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Player>> GetPlayersByPositionAsync(Position position)
        {
            return await _dbSet
                .Where(p => p.Position == position)
                .Include(p => p.CurrentClub)
                .Include(p => p.Agent)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByClubAsync(int clubId)
        {
            return await _dbSet
                .Where(p => p.CurrentClubId == clubId)
                .Include(p => p.Agent)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByAgentAsync(int agentId)
        {
            return await _dbSet
                .Where(p => p.AgentId == agentId)
                .Include(p => p.CurrentClub)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByNationalityAsync(string nationality)
        {
            return await _dbSet
                .Where(p => p.Nationality.ToLower() == nationality.ToLower())
                .Include(p => p.CurrentClub)
                .Include(p => p.Agent)
                .ToListAsync();
        }

        public async Task<Player?> GetPlayerWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.CurrentClub)
                .Include(p => p.Agent)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Player>> GetPlayersWithTransfersAsync()
        {
            return await _dbSet
                .Include(p => p.CurrentClub)
                .Include(p => p.Agent)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> SearchPlayersAsync(string searchTerm)
        {
            var term = searchTerm.ToLower();
            return await _dbSet
                .Where(p => p.FirstName.ToLower().Contains(term) || 
                           p.LastName.ToLower().Contains(term) ||
                           p.Nationality.ToLower().Contains(term))
                .Include(p => p.CurrentClub)
                .Include(p => p.Agent)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByAgeRangeAsync(int minAge, int maxAge)
        {
            var maxDate = DateTime.UtcNow.AddYears(-minAge);
            var minDate = DateTime.UtcNow.AddYears(-maxAge);
            
            return await _dbSet
                .Where(p => p.DateOfBirth >= minDate && p.DateOfBirth <= maxDate)
                .Include(p => p.CurrentClub)
                .Include(p => p.Agent)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByMarketValueRangeAsync(decimal minValue, decimal maxValue)
        {
            return await _dbSet
                .Where(p => p.MarketValue >= minValue && p.MarketValue <= maxValue)
                .Include(p => p.CurrentClub)
                .Include(p => p.Agent)
                .ToListAsync();
        }
    }
}