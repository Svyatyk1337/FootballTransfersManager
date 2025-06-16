using FootballTransfers.Core.Entities;

namespace FootballTransfers.Core.Interfaces
{
    public interface IAgentRepository : IGenericRepository<Agent>
    {
        Task<Agent?> GetAgentWithPlayersAsync(int id);
        Task<IEnumerable<Agent>> SearchAgentsAsync(string searchTerm);
        Task<Agent?> GetAgentByEmailAsync(string email);
        Task<IEnumerable<Agent>> GetAgentsWithMostPlayersAsync(int count);
    }
}