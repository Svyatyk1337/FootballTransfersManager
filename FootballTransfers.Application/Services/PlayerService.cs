using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;

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
    }
}
