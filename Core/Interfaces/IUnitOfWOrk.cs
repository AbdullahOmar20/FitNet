

using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWOrk : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();
    }
}