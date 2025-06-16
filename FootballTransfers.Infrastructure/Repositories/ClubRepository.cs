using Microsoft.EntityFrameworkCore;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;
using FootballTransfers.Infrastructure.Data;

namespace FootballTransfers.Infrastructure.Repositories
{
    public class ClubRepository : GenericRepository<Club>, IClubRepository
    {
        public ClubRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Club>> GetClubsByCountryAsync(string country)
        {
            return await _dbSet
                .Where(c => c.Country.ToLower() == country.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetClubsByLeagueAsync(string league)
        {
            return await _dbSet
                .Where(c => c.League.ToLower() == league.ToLower())
                .ToListAsync();
        }

        public async Task<Club?> GetClubWithPlayersAsync(int id)
        {
            return await _dbSet
                .Include(c => c.CurrentPlayers)
                    .ThenInclude(p => p.Agent)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Club?> GetClubWithTransfersAsync(int id)
        {
            return await _dbSet
                .Include(c => c.TransfersFrom)
                    .ThenInclude(t => t.Player)
                .Include(c => c.TransfersTo)
                    .ThenInclude(t => t.Player)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Club>> SearchClubsAsync(string searchTerm)
        {
            var term = searchTerm.ToLower();
            return await _dbSet
                .Where(c => c.Name.ToLower().Contains(term) ||
                           c.Country.ToLower().Contains(term) ||
                           c.League.ToLower().Contains(term) ||
                           c.City.ToLower().Contains(term))
                .ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetClubsWithMostTransfersAsync(int count)
        {
            return await _dbSet
                .Include(c => c.TransfersFrom)
                .Include(c => c.TransfersTo)
                .OrderByDescending(c => c.TransfersFrom.Count + c.TransfersTo.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}