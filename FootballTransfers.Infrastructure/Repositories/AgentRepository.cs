using Microsoft.EntityFrameworkCore;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;
using FootballTransfers.Infrastructure.Data;

namespace FootballTransfers.Infrastructure.Repositories
{
    public class AgentRepository : GenericRepository<Agent>, IAgentRepository
    {
        public AgentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Agent?> GetAgentWithPlayersAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Players)
                    .ThenInclude(p => p.CurrentClub)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Agent>> SearchAgentsAsync(string searchTerm)
        {
            var term = searchTerm.ToLower();
            return await _dbSet
                .Where(a => a.FirstName.ToLower().Contains(term) ||
                           a.LastName.ToLower().Contains(term) ||
                           (a.Company != null && a.Company.ToLower().Contains(term)))
                .Include(a => a.Players)
                .ToListAsync();
        }

        public async Task<Agent?> GetAgentByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<IEnumerable<Agent>> GetAgentsWithMostPlayersAsync(int count)
        {
            return await _dbSet
                .Include(a => a.Players)
                .OrderByDescending(a => a.Players.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}