using FootballTransfers.Application.DTOs;

namespace FootballTransfers.Application.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetAllAsync();
        Task<PlayerDto?> GetByIdAsync(int id);
        Task<PlayerDto> CreateAsync(CreatePlayerDto dto);
        Task UpdateAsync(int id, UpdatePlayerDto dto);
        Task DeleteAsync(int id);
    }
}