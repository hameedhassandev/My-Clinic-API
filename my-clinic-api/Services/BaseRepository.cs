using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        public ApplicationDbContext _Context { get; }
        public BaseRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _Context.Set<T>().Update(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }


        public async Task<int> CountAsync()
        {
            return await _Context.Set<T>().CountAsync();
        }

        public async Task<IEnumerable<T>> GetAllPaginationAsync(int skip, int take)
        {
            return await _Context.Set<T>().Skip(skip).Take(take).ToListAsync();

        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return await _Context.Set<T>().AsQueryable().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            return await _Context.Set<T>().AsQueryable().Where(criteria).ToListAsync();
        }



    }
}
