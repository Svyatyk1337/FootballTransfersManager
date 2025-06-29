using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Pagination;

namespace FootballTransfers.Application.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<ClubDto>> GetAllAsync();
        Task<ClubDto?> GetByIdAsync(int id);
        Task<ClubDto> CreateAsync(CreateClubDto dto);
        Task UpdateAsync(int id, UpdateClubDto dto);
        Task DeleteAsync(int id);

        Task<PagedResult<ClubDto>> GetPagedAsync(ClubFilterParams filter);
    }
}