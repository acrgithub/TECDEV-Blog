using Microsoft.EntityFrameworkCore;
using TECDEVBlog.Web.Data;
using TECDEVBlog.Web.Models.Domain;

namespace TECDEVBlog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext tECDEVBlogDbContext;

        public BlogPostRepository(BlogDbContext tECDEVBlogDbContext)
        {
            this.tECDEVBlogDbContext = tECDEVBlogDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await tECDEVBlogDbContext.AddAsync(blogPost);
            await tECDEVBlogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await tECDEVBlogDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                tECDEVBlogDbContext.BlogPosts.Remove(existingBlog);
                await tECDEVBlogDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await tECDEVBlogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await tECDEVBlogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await tECDEVBlogDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x =>x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await tECDEVBlogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await tECDEVBlogDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}
