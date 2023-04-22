using Microsoft.AspNetCore.Mvc;
using Projekt.Data.Data;
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
            return View(item);
        }
    }
}