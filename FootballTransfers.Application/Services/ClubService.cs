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
    public class ClubService : IClubService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClubService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClubDto>> GetAllAsync()
        {
            var clubs = await _unitOfWork.Clubs.GetAllAsync();
            return clubs.Select(c => new ClubDto
            {
                Id = c.Id,
                Name = c.Name,
                City = c.City,
                Country = c.Country,
                Stadium = c.Stadium,
                League = c.League,
                LogoUrl = c.LogoUrl,
                Founded = c.Founded,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            });
        }

        public async Task<ClubDto?> GetByIdAsync(int id)
        {
            var c = await _unitOfWork.Clubs.GetByIdAsync(id);
            return c == null ? null : new ClubDto
            {
                Id = c.Id,
                Name = c.Name,
                City = c.City,
                Country = c.Country,
                Stadium = c.Stadium,
                League = c.League,
                LogoUrl = c.LogoUrl,
                Founded = c.Founded,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            };
        }

        public async Task<ClubDto> CreateAsync(CreateClubDto dto)
        {
            var club = new Club
            {
                Name = dto.Name,
                City = dto.City,
                Country = dto.Country,
                Stadium = dto.Stadium,
                League = dto.League,
                LogoUrl = dto.LogoUrl,
                Founded = dto.Founded,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Clubs.AddAsync(club);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(club.Id) ?? throw new Exception("Club not found after creation");
        }

        public async Task UpdateAsync(int id, UpdateClubDto dto)
        {
            var club = await _unitOfWork.Clubs.GetByIdAsync(id);
            if (club == null) throw new Exception("Club not found");

            club.Name = dto.Name;
            club.City = dto.City;
            club.Country = dto.Country;
            club.Stadium = dto.Stadium;
            club.League = dto.League;
            club.LogoUrl = dto.LogoUrl;
            club.Founded = dto.Founded;
            club.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Clubs.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<PagedResult<ClubDto>> GetPagedAsync(ClubFilterParams filter)
            {
                var query = await _unitOfWork.Clubs.GetAllAsync();
                var filtered = query.AsQueryable();

                filtered = filter.SortBy?.ToLower() switch
                {
                    "name" => filter.Descending ? filtered.OrderByDescending(c => c.Name) : filtered.OrderBy(c => c.Name),
                    _ => filtered
                };

                var total = filtered.Count();
                var items = filtered
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();

                var result = items.Select(c => new ClubDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = c.Country,
                    Founded = c.Founded,
                    Stadium = c.Stadium,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }).ToList();

                return new PagedResult<ClubDto>
                {
                    Items = result,
                    TotalCount = total,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                };
            }

    }
}
