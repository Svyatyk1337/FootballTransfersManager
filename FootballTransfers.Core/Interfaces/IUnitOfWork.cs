namespace FootballTransfers.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPlayerRepository Players { get; }
        IClubRepository Clubs { get; }
        ITransferRepository Transfers { get; }
        IAgentRepository Agents { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}