using Microsoft.EntityFrameworkCore;
using TECDEVBlog.Web.Data;
using TECDEVBlog.Web.Models.Domain;

namespace TECDEVBlog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext tECDEVBlogDbContext;

        public TagRepository(BlogDbContext tECDEVBlogDbContext)
        {
            this.tECDEVBlogDbContext = tECDEVBlogDbContext;
        }


        public async Task<Tag> AddSync(Tag tag)
        {
            await tECDEVBlogDbContext.Tags.AddAsync(tag);
            await tECDEVBlogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await tECDEVBlogDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                tECDEVBlogDbContext.Tags.Remove(existingTag);
                await tECDEVBlogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await tECDEVBlogDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return tECDEVBlogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await tECDEVBlogDbContext.Tags.FindAsync(tag.Id);
           
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await tECDEVBlogDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;

        }
    }
}
