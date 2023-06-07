
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;

namespace Projekt.intranet.Controllers
{
    public class BlogPostsController : BaseController<BlogPost>
    {
        public BlogPostsController(ProjectContext context) : base(context)
        {
        }

        private bool BlogPostExists(int id)
        {
            return (_context.BlogPost?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public override async Task SetSelectList()
        {

            var pictures = await _context.Picture.ToListAsync();
            ViewBag.Pictures = new SelectList(pictures, "HeaderImageId", "Nazwa");
        }

        public override async Task<List<BlogPost>> getEntityList()
        {
            return await (
                          from blogPost in _context.BlogPost
                          where blogPost.IsActive == true
                          select blogPost
                  ).ToListAsync();
        }

        public override async Task<BlogPost> getElementById(int id)
        {
            return await _context.BlogPost
                .Include(b => b.HeaderImage)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
