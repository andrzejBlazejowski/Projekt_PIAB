using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class BlogPostController : BaseController
    {
        public BlogPostController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();

            var item = await _context.BlogPost.FindAsync(id);
            return View(item);
        }
    }
}