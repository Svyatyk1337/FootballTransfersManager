using Microsoft.EntityFrameworkCore;
using FootballTransfers.Core.Entities;
using FootballTransfers.Core.Interfaces;
using FootballTransfers.Infrastructure.Data;

namespace FootballTransfers.Infrastructure.Repositories
{
    public class TransferRepository : GenericRepository<Transfer>, ITransferRepository
    {
        public TransferRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Transfer>> GetTransfersByPlayerAsync(int playerId)
        {
            return await _dbSet
                .Where(t => t.PlayerId == playerId)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetTransfersByClubAsync(int clubId)
        {
            return await _dbSet
                .Where(t => t.FromClubId == clubId || t.ToClubId == clubId)
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetTransfersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(t => t.TransferDate >= startDate && t.TransferDate <= endDate)
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetTransfersByFeeRangeAsync(decimal minFee, decimal maxFee)
        {
            return await _dbSet
                .Where(t => t.TransferFee >= minFee && t.TransferFee <= maxFee)
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferFee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetTransfersByTypeAsync(TransferType transferType)
        {
            return await _dbSet
                .Where(t => t.TransferType == transferType)
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferDate)
                .ToListAsync();
        }

        public async Task<Transfer?> GetTransferWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Player)
                    .ThenInclude(p => p.Agent)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transfer>> GetRecentTransfersAsync(int count)
        {
            return await _dbSet
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetTopTransfersByFeeAsync(int count)
        {
            return await _dbSet
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferFee)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetConfirmedTransfersAsync()
        {
            return await _dbSet
                .Where(t => t.IsConfirmed)
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.TransferDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetPendingTransfersAsync()
        {
            return await _dbSet
                .Where(t => !t.IsConfirmed)
                .Include(t => t.Player)
                .Include(t => t.FromClub)
                .Include(t => t.ToClub)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}