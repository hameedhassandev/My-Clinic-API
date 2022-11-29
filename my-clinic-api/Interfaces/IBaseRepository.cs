namespace my_clinic_api.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CountAsync();
    }
}
