using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.Sharded;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class SiteController : BaseController
    {
        public SiteController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();

            var item = await _context.Site.FindAsync(id);
            return View(item);
        }

        public async Task<IActionResult> Details(int id)
        {
            prepareLayoutData();

            var item = await _context.Site.FindAsync(id);

            //TODO: Find out why linQ does not searches for image itself - mostlikelly enumerations error
            Picture HeaderImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == item.HeaderImageId);

            item.HeaderImage = HeaderImage;
            return View(item);
        }
    }
}