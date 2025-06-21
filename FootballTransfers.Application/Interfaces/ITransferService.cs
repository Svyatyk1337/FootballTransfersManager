using FootballTransfers.Application.DTOs;

namespace FootballTransfers.Application.Interfaces
{
    public interface ITransferService
    {
        Task<IEnumerable<TransferDto>> GetAllAsync();
        Task<TransferDto?> GetByIdAsync(int id);
        Task<TransferDto> CreateAsync(CreateTransferDto dto);
        Task UpdateAsync(int id, UpdateTransferDto dto);
        Task DeleteAsync(int id);
    }
}
