
using Microsoft.AspNetCore.Mvc;
using TECDEVBlog.Web.Models.ViewModels;
using TECDEVBlog.Web.Repositories;

namespace TECDEVBlog.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        

        public BlogsController(IBlogPostRepository blogPostRepository)
                        
        {
            this.blogPostRepository = blogPostRepository;   
        }


        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

            var blogDetailsViewModel = new BlogDetailsViewModel
            {
                Id = blogPost.Id,
                Content = blogPost.Content,
                PageTitle = blogPost.PageTitle,
                Author = blogPost.Author,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Heading = blogPost.Heading,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Visible = blogPost.Visible,
                Tags = blogPost.Tags,

            };
            

            return View(blogDetailsViewModel);
        }
    }
}
