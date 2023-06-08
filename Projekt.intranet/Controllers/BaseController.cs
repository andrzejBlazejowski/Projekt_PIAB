using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.Sharded;
namespace Projekt.intranet.Controllers
{
public abstract class BaseController<Entity> : Controller where Entity : BaseData
    {
        protected readonly ProjectContext _context;
        public BaseController(ProjectContext context)
        {
            _context = context;
        }
        public abstract Task<List<Entity>> getEntityList();
        public abstract Task<Entity> getElementById(int id);

        public virtual void removeElement(Entity element)
        {
            element.IsActive = false;
            _context.Update(element);
        }
        public virtual Entity setDefaultsEditValues(Entity element)
        {
            element.IsActive = true;
            element.LastModificationDate = DateTime.Now;
            element.LastModifiedBy = 1;
            element.CreationDate = DateTime.Now;
            element.CreatedBy = 1;
            return element;
        }

        public virtual Entity setDefaultsCreateValues(Entity element)
        {
            element.IsActive = true;
            element.LastModificationDate = DateTime.Now;
            element.LastModifiedBy = 1;
            element.CreationDate = DateTime.Now;
            element.CreatedBy = 1;
            return element;
        }

        public virtual Task SetSelectList()
        {
            return null;
        }

        public async Task<IActionResult> Index()
        {
            return View(await getEntityList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Entity entity)
        {
            entity = setDefaultsCreateValues(entity);
            try
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                throw;
            }
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
        public async Task<IActionResult> Edit(int id, Entity element)
        {
            element = setDefaultsEditValues(element);
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

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var element = await getElementById(id);
            if (element != null)
            {
                removeElement(element);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
