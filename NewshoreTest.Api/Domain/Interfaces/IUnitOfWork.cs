using NewshoreTest.Api.Infrastructure;

namespace NewshoreTest.Api.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ApplicationDbContext Context { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void CommitTransaction();
    }
}
