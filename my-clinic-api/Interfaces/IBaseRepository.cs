using System.Linq.Expressions;

namespace my_clinic_api.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllPaginationAsync(int skip, int take);
        Task<int> CountAsync();

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        //<Expression<func<Hospital,bool>> predict = p=>p.name == 'amr' && p.size > 33 ;
        //context.findAllAysync(predict,1,10);
    }
}
