using Microsoft.AspNetCore.Mvc;
using Projekt.Data.Data;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();

            var item = await _context.Product.FindAsync(id);
            return View(item);
        }
    }
}