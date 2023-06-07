using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.Shop;

namespace Projekt.intranet.Controllers
{
    public class ProductsController : BaseController<Product>
    {
        public ProductsController(ProjectContext context)
            : base(context) { }


        public override async Task<List<Product>> getEntityList()
        {
            return await 
                _context.Product.Where(p => p.IsActive == true)
                .Include(p => p.Image).Include(p => p.ProductCategory).ToListAsync();
        }

        public override async Task SetSelectList()
        {
            ViewData["ImageId"] = new SelectList(_context.Picture, "Id", "ImageData");
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategory, "Id", "LinkTitle");
        }

        public override async Task<Product> getElementById(int id)
        {
            return await _context.Product
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
