using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballTransfers.Application.DTOs;
using FootballTransfers.Application.Interfaces;
using FootballTransfers.Application.Pagination;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;

namespace FootballTransfers.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TransferDto>> GetAllAsync()
        {
            var transfers = await _unitOfWork.Transfers.GetAllAsync();
            return transfers.Select(t => new TransferDto
            {
                Id = t.Id,
                Player = new PlayerDto { Id = t.Player.Id, FirstName = t.Player.FirstName, LastName = t.Player.LastName },
                FromClub = t.FromClub != null ? new ClubDto { Id = t.FromClub.Id, Name = t.FromClub.Name } : null,
                ToClub = new ClubDto { Id = t.ToClub.Id, Name = t.ToClub.Name },
                TransferFee = t.TransferFee,
                TransferDate = t.TransferDate,
                TransferType = t.TransferType,
                ContractLength = t.ContractLength,
                Description = t.Description,
                IsConfirmed = t.IsConfirmed,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            });
        }

        public async Task<TransferDto?> GetByIdAsync(int id)
        {
            var t = await _unitOfWork.Transfers.GetByIdAsync(id);
            return t == null ? null : new TransferDto
            {
                Id = t.Id,
                Player = new PlayerDto { Id = t.Player.Id, FirstName = t.Player.FirstName, LastName = t.Player.LastName },
                FromClub = t.FromClub != null ? new ClubDto { Id = t.FromClub.Id, Name = t.FromClub.Name } : null,
                ToClub = new ClubDto { Id = t.ToClub.Id, Name = t.ToClub.Name },
                TransferFee = t.TransferFee,
                TransferDate = t.TransferDate,
                TransferType = t.TransferType,
                ContractLength = t.ContractLength,
                Description = t.Description,
                IsConfirmed = t.IsConfirmed,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            };
        }

        public async Task<TransferDto> CreateAsync(CreateTransferDto dto)
        {
            var transfer = new Transfer
            {
                PlayerId = dto.PlayerId,
                FromClubId = dto.FromClubId,
                ToClubId = dto.ToClubId,
                TransferFee = dto.TransferFee,
                TransferDate = dto.TransferDate,
                TransferType = dto.TransferType,
                ContractLength = dto.ContractLength,
                Description = dto.Description,
                IsConfirmed = dto.IsConfirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Transfers.AddAsync(transfer);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(transfer.Id) ?? throw new Exception("Transfer not found after creation");
        }

        public async Task UpdateAsync(int id, UpdateTransferDto dto)
        {
            var transfer = await _unitOfWork.Transfers.GetByIdAsync(id);
            if (transfer == null) throw new Exception("Transfer not found");

            transfer.PlayerId = dto.PlayerId;
            transfer.FromClubId = dto.FromClubId;
            transfer.ToClubId = dto.ToClubId;
            transfer.TransferFee = dto.TransferFee;
            transfer.TransferDate = dto.TransferDate;
            transfer.TransferType = dto.TransferType;
            transfer.ContractLength = dto.ContractLength;
            transfer.Description = dto.Description;
            transfer.IsConfirmed = dto.IsConfirmed;
            transfer.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Transfers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<PagedResult<TransferDto>> GetPagedAsync(TransferFilterParams filter)
            {
                var query = await _unitOfWork.Transfers.GetAllAsync();
                var filtered = query.AsQueryable();

                if (filter.MinFee.HasValue)
                    filtered = filtered.Where(t => t.TransferFee >= filter.MinFee.Value);

                if (filter.MaxFee.HasValue)
                    filtered = filtered.Where(t => t.TransferFee <= filter.MaxFee.Value);

                if (filter.FromDate.HasValue)
                    filtered = filtered.Where(t => t.TransferDate >= filter.FromDate.Value);

                if (filter.ToDate.HasValue)
                    filtered = filtered.Where(t => t.TransferDate <= filter.ToDate.Value);

                filtered = filter.SortBy?.ToLower() switch
                {
                    "transferfee" => filter.Descending ? filtered.OrderByDescending(t => t.TransferFee) : filtered.OrderBy(t => t.TransferFee),
                    "transferdate" => filter.Descending ? filtered.OrderByDescending(t => t.TransferDate) : filtered.OrderBy(t => t.TransferDate),
                    _ => filter.Descending ? filtered.OrderByDescending(t => t.Id) : filtered.OrderBy(t => t.Id),
                };

                var total = filtered.Count();
                var items = filtered
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();

                var result = items.Select(t => new TransferDto
                {
                    Id = t.Id,
                    TransferDate = t.TransferDate,
                    TransferFee = t.TransferFee,
                    TransferType = t.TransferType,
                    ContractLength = t.ContractLength,
                    Description = t.Description,
                    IsConfirmed = t.IsConfirmed,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    Player = new PlayerDto { Id = t.Player.Id, FirstName = t.Player.FirstName, LastName = t.Player.LastName },
                    FromClub = t.FromClub != null ? new ClubDto { Id = t.FromClub.Id, Name = t.FromClub.Name } : null,
                    ToClub = new ClubDto { Id = t.ToClub.Id, Name = t.ToClub.Name }
                }).ToList();

            return new PagedResult<TransferDto>
            {
                Items = result,
                TotalCount = total,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
            }
    }
}
