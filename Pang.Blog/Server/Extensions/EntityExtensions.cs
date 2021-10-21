using System.Collections.Generic;
using System.Linq;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Extensions
{
    public static class EntityExtensions
    {
        public static string ToLineString<T>(this IEnumerable<T> list) where T : EntityBase
        {
            return string.Join(",", list.Select(x => x.Id));
        }
    }
}