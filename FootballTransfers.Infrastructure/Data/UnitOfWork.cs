using Microsoft.EntityFrameworkCore.Storage;
using FootballTransfers.Core.Interfaces;
using FootballTransfers.Infrastructure.Repositories;

namespace FootballTransfers.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Players = new PlayerRepository(_context);
            Clubs = new ClubRepository(_context);
            Transfers = new TransferRepository(_context);
            Agents = new AgentRepository(_context);
        }

        public IPlayerRepository Players { get; }
        public IClubRepository Clubs { get; }
        public ITransferRepository Transfers { get; }
        public IAgentRepository Agents { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}