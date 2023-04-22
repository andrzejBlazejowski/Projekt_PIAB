using Microsoft.AspNetCore.Mvc;
using Projekt.Data.Data;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class ShopController : BaseController
    {
        public ShopController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();

            return View((
                from Product in _context.Product
                select Product
            ).ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            prepareLayoutData();

            var item = await _context.Product.FindAsync(id);
            return View(item);
        }
    }
}