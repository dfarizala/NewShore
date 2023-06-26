using NewshoreTest.Api.Domain.Interfaces;

namespace NewshoreTest.Api.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ApplicationDbContext Context { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            Context = context;
        }

        void IDisposable.Dispose()
        {
            Context.Dispose();
        }

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        void IUnitOfWork.CommitTransaction()
        {
            Context.SaveChanges();
        }
    }
}
