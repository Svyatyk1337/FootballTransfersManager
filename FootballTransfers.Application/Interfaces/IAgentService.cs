using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Pagination;

namespace FootballTransfers.Application.Interfaces
{
    public interface IAgentService
    {
        Task<IEnumerable<AgentDto>> GetAllAsync();
        Task<AgentDto?> GetByIdAsync(int id);
        Task<AgentDto> CreateAsync(CreateAgentDto dto);
        Task UpdateAsync(int id, UpdateAgentDto dto);
        Task DeleteAsync(int id);

        Task<PagedResult<AgentDto>> GetPagedAsync(AgentFilterParams filter);
    }
}