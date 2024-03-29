
using Microsoft.EntityFrameworkCore;
using TECDEVBlog.Web.Models.Domain;

namespace TECDEVBlog.Web.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    

    }
}
