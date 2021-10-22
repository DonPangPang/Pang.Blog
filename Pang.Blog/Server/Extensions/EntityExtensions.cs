using System.Collections.Generic;
using System.Linq;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Extensions
{
    /// <summary>
    /// 实体扩展
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// 将实体集合的Id转为字符串
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToLineString<T>(this IEnumerable<T> list) where T : EntityBase
        {
            return string.Join(",", list.Select(x => x.Id));
        }
    }
}