using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
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

        public async Task<T> Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _Context.Set<T>().Update(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            if (entity == null) return null;

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
        public async Task<IEnumerable<T>> GetAllWithData()
        {
            var query = _Context.Set<T>().AsQueryable();
            var includes = GetCollections(typeof(T));
            foreach(var inc in includes)
            {
                query = query.Include(inc);
            }
            return await query.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return await _Context.Set<T>().AsQueryable().SingleOrDefaultAsync(criteria);
        }
        public async Task<T> FindWithData(Expression<Func<T, bool>> criteria)
        {
            var col = GetCollections(typeof(T));
            var query = _Context.Set<T>().AsQueryable();
            foreach (var inc in col)
            {
                query = query.Include(inc);
            }
            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            return await _Context.Set<T>().AsQueryable().Where(criteria).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllWithData(Expression<Func<T, bool>> criteria)
        {
            var col = GetCollections(typeof(T));
            var query = _Context.Set<T>().AsQueryable().Where(criteria);
            foreach (var inc in col)
            {
                query = query.Include(inc);
            }
            return await query.Where(criteria).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllPaginationAsync(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return await _Context.Set<T>().AsQueryable().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        

        public void CommitChanges()
        {
            _Context.SaveChanges();
        }
        public List<string> GetCollections(Type entityType)
        {
            var col = entityType.GetProperties()
                                .Where(p => (typeof(IEnumerable).IsAssignableFrom(p.PropertyType)
                                    && p.PropertyType != typeof(string))
                                    && p.PropertyType != typeof(byte[])
                                    || p.PropertyType.Namespace == entityType.Namespace)
                                .Select(p => p.Name)
                                .ToList();
            return col;
            
        }
        



    }
}
