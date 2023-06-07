using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.Shop;

namespace Projekt.intranet.Controllers
{
public abstract class BaseController<BaseData> : Controller
    {
        protected readonly ProjectContext _context;
        public BaseController(ProjectContext context)
        {
            _context = context;
        }
        public abstract Task<List<BaseData>> getEntityList();
        public abstract Task<BaseData> getElementById(int id);

        public async Task<IActionResult> Index()
        {
            return View(await getEntityList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BaseData entity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public virtual Task SetSelectList()
        {
            return null;
        }

        public async Task<IActionResult> Create()
        {
            await SetSelectList();
            return View();
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = getElementById();
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

    }
}
