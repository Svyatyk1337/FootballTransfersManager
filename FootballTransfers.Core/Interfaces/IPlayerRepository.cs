using FootballTransfers.Core.Entities;

namespace FootballTransfers.Core.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<IEnumerable<Player>> GetPlayersByPositionAsync(Position position);
        Task<IEnumerable<Player>> GetPlayersByClubAsync(int clubId);
        Task<IEnumerable<Player>> GetPlayersByAgentAsync(int agentId);
        Task<IEnumerable<Player>> GetPlayersByNationalityAsync(string nationality);
        Task<Player?> GetPlayerWithDetailsAsync(int id);
        Task<IEnumerable<Player>> GetPlayersWithTransfersAsync();
        Task<IEnumerable<Player>> SearchPlayersAsync(string searchTerm);
        Task<IEnumerable<Player>> GetPlayersByAgeRangeAsync(int minAge, int maxAge);
        Task<IEnumerable<Player>> GetPlayersByMarketValueRangeAsync(decimal minValue, decimal maxValue);
    }
}