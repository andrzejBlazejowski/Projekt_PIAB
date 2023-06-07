using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await getElementById((int)id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await getElementById((int)id);
            if (element == null)
            {
                return NotFound();
            }
            await SetSelectList();
            return View(element);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BaseData element)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(element);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await SetSelectList();
            return View(element);
        }

    }
}
