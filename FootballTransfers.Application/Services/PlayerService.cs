using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;
using FootballTransfers.Application.Pagination;

namespace FootballTransfers.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PlayerDto>> GetAllAsync()
        {
            var players = await _unitOfWork.Players.GetAllAsync();
            return players.Select(p => new PlayerDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Position = p.Position,
                Height = p.Height,
                Weight = p.Weight,
                DateOfBirth = p.DateOfBirth,
                MarketValue = p.MarketValue,
                Nationality = p.Nationality,
                AgentId = p.AgentId,
                CurrentClubId = p.CurrentClubId,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Agent = p.Agent != null ? new AgentDto { Id = p.Agent.Id, FirstName = p.Agent.FirstName, LastName = p.Agent.LastName } : null,
                CurrentClub = p.CurrentClub != null ? new ClubDto { Id = p.CurrentClub.Id, Name = p.CurrentClub.Name } : null
            });
        }

        public async Task<PlayerDto?> GetByIdAsync(int id)
        {
            var p = await _unitOfWork.Players.GetByIdAsync(id);
            return p == null ? null : new PlayerDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Position = p.Position,
                Height = p.Height,
                Weight = p.Weight,
                DateOfBirth = p.DateOfBirth,
                MarketValue = p.MarketValue,
                Nationality = p.Nationality,
                AgentId = p.AgentId,
                CurrentClubId = p.CurrentClubId,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Agent = p.Agent != null ? new AgentDto { Id = p.Agent.Id, FirstName = p.Agent.FirstName, LastName = p.Agent.LastName } : null,
                CurrentClub = p.CurrentClub != null ? new ClubDto { Id = p.CurrentClub.Id, Name = p.CurrentClub.Name } : null
            };
        }

        public async Task<PlayerDto> CreateAsync(CreatePlayerDto dto)
        {
            var player = new Player
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Position = dto.Position,
                Height = dto.Height,
                Weight = dto.Weight,
                DateOfBirth = dto.DateOfBirth,
                MarketValue = dto.MarketValue,
                Nationality = dto.Nationality,
                AgentId = dto.AgentId,
                CurrentClubId = dto.CurrentClubId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _unitOfWork.Players.AddAsync(player);
            await _unitOfWork.SaveChangesAsync();
            return await GetByIdAsync(player.Id) ?? throw new Exception("Player not found after creation");
        }

        public async Task UpdateAsync(int id, UpdatePlayerDto dto)
        {
            var player = await _unitOfWork.Players.GetByIdAsync(id);
            if (player == null) throw new Exception("Player not found");

            player.FirstName = dto.FirstName;
            player.LastName = dto.LastName;
            player.Position = dto.Position;
            player.Height = dto.Height;
            player.Weight = dto.Weight;
            player.DateOfBirth = dto.DateOfBirth;
            player.MarketValue = dto.MarketValue;
            player.Nationality = dto.Nationality;
            player.AgentId = dto.AgentId;
            player.CurrentClubId = dto.CurrentClubId;
            player.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Players.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<PagedResult<PlayerDto>> GetPagedAsync(PlayerFilterParams filter)
        {
            var query = await _unitOfWork.Players.GetAllAsync();
            
            var filtered = query.AsQueryable();

            if (filter.Position.HasValue)
                filtered = filtered.Where(p => p.Position == filter.Position.Value);

            if (filter.MinAge.HasValue)
                filtered = filtered.Where(p => p.DateOfBirth <= DateTime.Today.AddYears(-filter.MinAge.Value));

            if (filter.MaxAge.HasValue)
                filtered = filtered.Where(p => p.DateOfBirth >= DateTime.Today.AddYears(-filter.MaxAge.Value));

            if (filter.MinMarketValue.HasValue)
                filtered = filtered.Where(p => p.MarketValue >= filter.MinMarketValue.Value);

            if (filter.MaxMarketValue.HasValue)
                filtered = filtered.Where(p => p.MarketValue <= filter.MaxMarketValue.Value);

            filtered = filter.SortBy?.ToLower() switch
            {
                "firstname" => filter.IsDescending ? filtered.OrderByDescending(p => p.FirstName) : filtered.OrderBy(p => p.FirstName),
                "dateofbirth" => filter.IsDescending ? filtered.OrderByDescending(p => p.DateOfBirth) : filtered.OrderBy(p => p.DateOfBirth),
                "marketvalue" => filter.IsDescending ? filtered.OrderByDescending(p => p.MarketValue) : filtered.OrderBy(p => p.MarketValue),
                _ => filter.IsDescending ? filtered.OrderByDescending(p => p.LastName) : filtered.OrderBy(p => p.LastName),
            };

            var total = filtered.Count();
            var items = filtered
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var result = items.Select(p => new PlayerDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Position = p.Position,
                Height = p.Height,
                Weight = p.Weight,
                DateOfBirth = p.DateOfBirth,
                MarketValue = p.MarketValue,
                Nationality = p.Nationality,
                AgentId = p.AgentId,
                CurrentClubId = p.CurrentClubId,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Agent = p.Agent != null ? new AgentDto { Id = p.Agent.Id, FirstName = p.Agent.FirstName, LastName = p.Agent.LastName } : null,
                CurrentClub = p.CurrentClub != null ? new ClubDto { Id = p.CurrentClub.Id, Name = p.CurrentClub.Name } : null
            }).ToList();

            return new PagedResult<PlayerDto>
            {
                Items = result,
                TotalCount = total,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
    }
}
