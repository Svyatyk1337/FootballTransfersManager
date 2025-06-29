using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using FootballTransfers.Application.Pagination;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;

namespace FootballTransfers.Application.Services
{
    public class AgentService : IAgentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AgentDto>> GetAllAsync()
        {
            var agents = await _unitOfWork.Agents.GetAllAsync();
            return agents.Select(a => new AgentDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Company = a.Company,
                Email = a.Email,
                Phone = a.Phone,
                PlayersCount = a.Players?.Count ?? 0,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            });
        }

        public async Task<AgentDto?> GetByIdAsync(int id)
        {
            var a = await _unitOfWork.Agents.GetByIdAsync(id);
            return a == null ? null : new AgentDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Company = a.Company,
                Email = a.Email,
                Phone = a.Phone,
                PlayersCount = a.Players?.Count ?? 0,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            };
        }

        public async Task<AgentDto> CreateAsync(CreateAgentDto dto)
        {
            var agent = new Agent
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Company = dto.Company,
                Email = dto.Email,
                Phone = dto.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Agents.AddAsync(agent);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(agent.Id) ?? throw new Exception("Agent not found after creation");
        }

        public async Task UpdateAsync(int id, UpdateAgentDto dto)
        {
            var agent = await _unitOfWork.Agents.GetByIdAsync(id);
            if (agent == null) throw new Exception("Agent not found");

            agent.FirstName = dto.FirstName;
            agent.LastName = dto.LastName;
            agent.Company = dto.Company;
            agent.Email = dto.Email;
            agent.Phone = dto.Phone;
            agent.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Agents.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<PagedResult<AgentDto>> GetPagedAsync(AgentFilterParams filter)
            {
                var query = await _unitOfWork.Agents.GetAllAsync();
                var filtered = query.AsQueryable();

                filtered = filter.SortBy?.ToLower() switch
                {
                    "firstname" => filter.Descending ? filtered.OrderByDescending(a => a.FirstName) : filtered.OrderBy(a => a.FirstName),
                    "lastname" => filter.Descending ? filtered.OrderByDescending(a => a.LastName) : filtered.OrderBy(a => a.LastName),
                    _ => filtered
                };

                var total = filtered.Count();
                var items = filtered
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();

                var result = items.Select(a => new AgentDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Company = a.Company,
                    Email = a.Email,
                    Phone = a.Phone,
                    PlayersCount = a.Players.Count,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                }).ToList();

                return new PagedResult<AgentDto>
                {
                    Items = result,
                    TotalCount = total,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                };
            }

    }
}
