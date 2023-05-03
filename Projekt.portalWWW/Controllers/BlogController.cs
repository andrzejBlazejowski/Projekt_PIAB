using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Projekt.Data.Data;
using Projekt.Data.Data.Sharded;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class BlogController : BaseController
    {
        public BlogController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();

            //TODO: Find out why linQ does not searches for image itself - mostlikelly enumerations error
            var blogPosts = (from BlogPost in _context.BlogPost
                             where BlogPost.IsActive == true
                             select BlogPost).ToList();

            foreach (var post in blogPosts)
            {
                Picture HeaderImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == post.HeaderImageId);
                post.HeaderImage = HeaderImage;
            }
            return View(blogPosts);
        }

        public async Task<IActionResult> Details(int id)
        {
            prepareLayoutData();

            var item = await _context.BlogPost.FindAsync(id);

            //TODO: Find out why linQ does not searches for image itself - mostlikelly enumerations error
            Picture HeaderImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == item.HeaderImageId);

            item.HeaderImage = HeaderImage;
            return View(item);
        }
    }
}