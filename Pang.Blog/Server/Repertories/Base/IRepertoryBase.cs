using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Repertories.Base
{
    public interface IRepertoryBase<T> where T: EntityBase
    {
        DbSet<T> DbSet { get; }
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(T entity);
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// /// <returns></returns>
        Task<bool> InsertAsync(IEnumerable<T> entities);

        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(IEnumerable<T> entities);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> query);

        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateAsync(IEnumerable<T> entities);

        Task<T> GetEntityByIdAsync(Guid id);
        Task<IEnumerable<T>> GetEntitiesAsync();
        Task<T> GetEntityAsync(Expression<Func<T, bool>> query);
        Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> query);

        Task<bool> ExistsAsync(Guid id);

        Task<bool> SaveAsync();
    }
}