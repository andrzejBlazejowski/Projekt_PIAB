using Microsoft.AspNetCore.Mvc;
using Projekt.Data.Data;

namespace Projekt.portalWWW.Controllers
{
    public class CheckoutItemController : BaseController
    {
        public CheckoutItemController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index()
        {
            prepareLayoutData();

            var items = await _context.CheckoutItem.FindAsync();
            return View(items);
        }
    }
}
