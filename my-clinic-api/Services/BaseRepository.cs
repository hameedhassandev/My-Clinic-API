using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System;
using System.Collections;
using System.Collections.Generic;
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
            var entity = await _Context.Set<T>().FindAsync(id);
            if (entity == null) return null;

            _Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<T> GetByIdWithIncludeAsync(int id , string Include , string navType)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            if (entity == null) return null;
            if (navType == "Collection")
                _Context.Entry(entity).Collection(Include).Load();
            else if(navType == "Reference")
                _Context.Entry(entity).Reference(Include).Load();
            _Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }


        public async Task<int> CountAsync()
        {
            return await _Context.Set<T>().CountAsync();
        }

        public async Task<IEnumerable<T>> GetAllPaginationAsync(int skip, int take)
        {
            return await _Context.Set<T>().Skip(skip).Take(take).ToListAsync();

        }
        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(List<string> Includes)
        {
            var query = _Context.Set<T>().AsQueryable();
            foreach(var inc in Includes)
            {
                query = query.Include(inc);
            }
            return await query.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return await _Context.Set<T>().AsQueryable().SingleOrDefaultAsync(criteria);
        }
        public async Task<T> FindWithIncludesAsync(Expression<Func<T, bool>> criteria , List<string> Includes)
        {
            var query = _Context.Set<T>().AsQueryable();
            foreach (var inc in Includes)
            {
                query = query.Include(inc);
            }
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>> FindAllPaginationAsync(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return await _Context.Set<T>().AsQueryable().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            return await _Context.Set<T>().AsQueryable().Where(criteria).ToListAsync();
        }

        public void CommitChanges()
        {
            _Context.SaveChanges();
        }

    }
}
