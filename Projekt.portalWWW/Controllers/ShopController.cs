using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;
using Projekt.Data.Data.Sharded;
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

            var products = (
                from Product in _context.Product
                select Product
            ).ToList();

            foreach (var product in products)
            {
                Picture HeaderImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == product.ImageId);
                product.Image = HeaderImage;
            }

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            prepareLayoutData();

            var item = await _context.Product.FindAsync(id);
            //TODO: Find out why linQ does not searches for image itself - mostlikelly enumerations error
            Picture HeaderImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == item.ImageId);

            item.Image = HeaderImage;

            return View(item);
        }
    }
}