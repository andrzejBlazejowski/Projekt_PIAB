using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.Sharded;
using Projekt.Data.Data.Shop;

namespace Projekt.portalWWW.Controllers
{
    public class ShopController : BaseController
    {
        public ShopController(ProjectContext context) : base(context) { }

        [HttpGet("Index/{categoryId}")]
        public async override Task<IActionResult> Index(int categoryId)
        {
            prepareLayoutData();

            var products = (
                from Product in _context.Product
                where Product.IsActive && Product.IsVisible && 
                    (Product.ProductCategoryId == categoryId || categoryId == 0) 
                select Product
            ).OrderBy(p => p.CreationDate).ToList();

            foreach (var product in products)
            {
                Picture HeaderImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == product.ImageId);
                product.Image = HeaderImage;
            }

            return View(products);
        }

        [HttpGet("Index/{categoryId}/{sortOrder?}/{minimumPrice?}/{maximumPrice?}")]
        public async Task<IActionResult> Index(int categoryId, string sortOrder, decimal? minimumPrice, decimal? maximumPrice)
        {
            prepareLayoutData();

            var productsQuery = _context.Product
                .Where(p => p.IsActive && p.IsVisible && (p.ProductCategoryId == categoryId || categoryId == 0));

            if (minimumPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minimumPrice.Value);
            }

            if (maximumPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= maximumPrice.Value);
            }

            switch (sortOrder)
            {
                case "Name":
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
                case "Price":
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case "dateAdded":
                default:
                    productsQuery = productsQuery.OrderBy(p => p.CreationDate);
                    break;
            }

            var products = await productsQuery.ToListAsync();

            foreach (var product in products)
            {
                Picture headerImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == product.ImageId);
                product.Image = headerImage;
            }

            return View(products);
        }


        [HttpGet("Index/{categoryId}/{searchString}")]
        public async Task<IActionResult> Index(int categoryId, string searchString)
        {
            prepareLayoutData();

            var productsQuery = _context.Product
                .Where(p => p.IsActive && p.IsVisible && (p.ProductCategoryId == categoryId || categoryId == 0));

            if (!string.IsNullOrEmpty(searchString))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString) || p.Content.Contains(searchString));
            }

            var products = await productsQuery.ToListAsync();

            foreach (var product in products)
            {
                Picture headerImage = await _context.Picture.FirstOrDefaultAsync(p => p.Id == product.ImageId);
                product.Image = headerImage;
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