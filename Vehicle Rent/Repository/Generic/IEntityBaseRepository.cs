using System.Linq.Expressions;

namespace Vehicle_Rent.Repository.Generic
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(string id);
        Task<T> GetByIdAsync(string id, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
        Task SaveChangesAsync();
    }
}
