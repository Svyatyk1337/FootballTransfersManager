using FootballTransfers.Core.Entities;

namespace FootballTransfers.Core.Interfaces
{
    public interface IClubRepository : IGenericRepository<Club>
    {
        Task<IEnumerable<Club>> GetClubsByCountryAsync(string country);
        Task<IEnumerable<Club>> GetClubsByLeagueAsync(string league);
        Task<Club?> GetClubWithPlayersAsync(int id);
        Task<Club?> GetClubWithTransfersAsync(int id);
        Task<IEnumerable<Club>> SearchClubsAsync(string searchTerm);
        Task<IEnumerable<Club>> GetClubsWithMostTransfersAsync(int count);
    }
}