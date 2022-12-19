using System.Linq.Expressions;

namespace my_clinic_api.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> FindByIdAsync(int id);
        Task<T> FindByIdWithIncludeAsync(int id , string Include, string navType);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllPaginationAsync(int skip, int take);
        Task<IEnumerable<T>> GetAllWithIncludeAsync(List<string> Includes);

        Task<int> CountAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindWithIncludesAsync(Expression<Func<T, bool>> criteria, List<string> Includes);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria);

        Task<IEnumerable<T>> FindAllPaginationAsync(Expression<Func<T, bool>> criteria, int skip, int take);

         void CommitChanges();
    }
}
