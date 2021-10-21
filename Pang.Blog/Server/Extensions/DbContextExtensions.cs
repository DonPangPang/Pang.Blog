using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pang.Blog.Server.Data;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Extensions
{
    public static class DbContextExtensions
    {
        public static ModelBuilder AddEntityTypes(this ModelBuilder modelBuilder)
        {
            var types = typeof(EntityBase).Assembly.GetTypes().AsEnumerable();
            var entityTypes = types.Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(EntityBase)));

            foreach (var type in entityTypes)
            {
                if (modelBuilder.Model.FindEntityType(type) is null)
                {
                    modelBuilder.Model.AddEntityType(type);
                }
            }

            return modelBuilder;
        }
    }
}