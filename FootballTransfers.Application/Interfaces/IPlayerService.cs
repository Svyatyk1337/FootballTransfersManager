using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Pagination;

namespace FootballTransfers.Application.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetAllAsync();
        Task<PlayerDto?> GetByIdAsync(int id);
        Task<PlayerDto> CreateAsync(CreatePlayerDto dto);
        Task UpdateAsync(int id, UpdatePlayerDto dto);
        Task DeleteAsync(int id);

        Task<PagedResult<PlayerDto>> GetPagedAsync(PlayerFilterParams filter);

    }
}