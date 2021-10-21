using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pang.Blog.Server.Data;
using Pang.Blog.Server.Entities.Base;
using Pang.Blog.Server.Extensions;

namespace Pang.Blog.Server.Repertories.Base
{
    public class RepertoryBase<T>: IRepertoryBase<T> where T: EntityBase
    {
        protected readonly BlogDbContext Context;
        public DbSet<T> DbSet { get; }

        public RepertoryBase(BlogDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        
        public async Task<bool> InsertAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.Create();
            await DbSet.AddAsync(entity);

            return await SaveAsync();
        }

        public async Task<bool> InsertAsync(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                entity.Create();
            }

            await DbSet.AddRangeAsync(entities);

            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            await Task.Run(() => DbSet.Remove(entity));
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => DbSet.RemoveRange(entities));
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> query)
        {
            var entities = await (DbSet as IQueryable<T>).Where(query).ToListAsync();
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            await DeleteAsync(entities);

            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await Task.Run(() => Context.Entry(entity).State = EntityState.Modified);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                await UpdateAsync(entity);
            }
            return await SaveAsync();
        }

        public async Task<T> GetEntityByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> GetEntitiesAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> query)
        {
            var queryExpression = DbSet as IQueryable<T>;
            return await queryExpression.Where(query).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> query)
        {
            var queryExpression = DbSet as IQueryable<T>;
            return await queryExpression.Where(query).ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await DbSet.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            return await Context.SaveChangesAsync() >= 0;
        }
    }
}