
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.Shop;

namespace Projekt.intranet.Controllers
{
    public class CheckoutItemsController : BaseController<CheckoutItem>
    {
        public CheckoutItemsController(ProjectContext context) : base(context)
        {
        }

        public override async Task SetSelectList()
        {
            ViewData["Product"] = new SelectList(_context.Product, "Id", "Name");
        }

        public override async Task<List<CheckoutItem>> getEntityList()
        {
            return await (
                          from cItem in _context.CheckoutItem
                          where cItem.IsActive == true
                          select cItem
                  ).ToListAsync();
        }

        public override async Task<CheckoutItem> getElementById(int id)
        {
            return await _context.CheckoutItem
                .Include(b => b.Name)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
