using Microsoft.EntityFrameworkCore;
using NewshoreTest.Api.Domain.Interfaces;
using System.Linq.Expressions;

namespace NewshoreTest.Api.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task IRepository<T>.AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _unitOfWork.Context.Set<T>().AddAsync(entity);
        }

        async Task IRepository<T>.DeleteAsync(T entity)
        {
            _unitOfWork.Context.Set<T>().Remove(entity);
        }

        void IRepository<T>.AddRange(List<T> entity, CancellationToken cancellationToken)
        {
            _unitOfWork.Context.Set<T>().AddRange(entity.ToArray());
        }

        void IRepository<T>.DeleteRange(List<T> entity)
        {
            _unitOfWork.Context.Set<T>().RemoveRange(entity.ToArray());
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            return (IQueryable<T>)_unitOfWork.Context.Set<T>().ToList();
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAsync()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAsync(Expression<Func<T, bool>> whereCondition, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties)
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();

            if (whereCondition != null) query = query.Where(whereCondition);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        async Task IRepository<T>.UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _unitOfWork.Context.Set<T>().Entry(entity).State = EntityState.Modified;
        }

    }
}
