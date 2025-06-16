using FootballTransfers.Core.Entities;

namespace FootballTransfers.Core.Interfaces
{
    public interface ITransferRepository : IGenericRepository<Transfer>
    {
        Task<IEnumerable<Transfer>> GetTransfersByPlayerAsync(int playerId);
        Task<IEnumerable<Transfer>> GetTransfersByClubAsync(int clubId);
        Task<IEnumerable<Transfer>> GetTransfersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transfer>> GetTransfersByFeeRangeAsync(decimal minFee, decimal maxFee);
        Task<IEnumerable<Transfer>> GetTransfersByTypeAsync(TransferType transferType);
        Task<Transfer?> GetTransferWithDetailsAsync(int id);
        Task<IEnumerable<Transfer>> GetRecentTransfersAsync(int count);
        Task<IEnumerable<Transfer>> GetTopTransfersByFeeAsync(int count);
        Task<IEnumerable<Transfer>> GetConfirmedTransfersAsync();
        Task<IEnumerable<Transfer>> GetPendingTransfersAsync();
    }
}