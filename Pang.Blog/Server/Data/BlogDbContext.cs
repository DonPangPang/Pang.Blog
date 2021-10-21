using Microsoft.EntityFrameworkCore;
using Pang.Blog.Server.Extensions;

namespace Pang.Blog.Server.Data
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityTypes();

            base.OnModelCreating(modelBuilder);
        }
    }
}