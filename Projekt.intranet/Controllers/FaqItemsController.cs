using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;

namespace Projekt.intranet.Controllers
{
    public class FaqItemsController : Controller
    {
        private readonly ProjectContext _context;

        public FaqItemsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: FaqItems
        public async Task<IActionResult> Index()
        {
            return _context.FaqItem != null ?
                        View(await (
                          from item in _context.FaqItem
                          where item.IsActive == true
                          select item
                  ).ToListAsync()) :
                          Problem("Entity set 'ProjectContext.FaqItem'  is null.");
        }

        // GET: FaqItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FaqItem == null)
            {
                return NotFound();
            }

            var faqItem = await _context.FaqItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faqItem == null)
            {
                return NotFound();
            }

            return View(faqItem);
        }

        // GET: FaqItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FaqItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,MetaTitle,MetaDescription,Id,Name,Description,IsActive,LastModificationDate,LastModifiedBy,CreationDate,CreatedBy")] FaqItem faqItem)
        {
            faqItem.LastModificationDate = DateTime.Now;
            faqItem.LastModifiedBy = 1;
            faqItem.CreationDate = DateTime.Now;
            faqItem.CreatedBy = 1;
            faqItem.IsActive = true;
            if (ModelState.IsValid)
            {
                _context.Add(faqItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faqItem);
        }

        // GET: FaqItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FaqItem == null)
            {
                return NotFound();
            }

            var faqItem = await _context.FaqItem.FindAsync(id);
            if (faqItem == null)
            {
                return NotFound();
            }
            return View(faqItem);
        }

        // POST: FaqItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Content,MetaTitle,MetaDescription,Id,Name,Description,IsActive,LastModificationDate,LastModifiedBy,CreationDate,CreatedBy")] FaqItem faqItem)
        {
            faqItem.LastModificationDate = DateTime.Now;
            faqItem.LastModifiedBy = 1;
            if (id != faqItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faqItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqItemExists(faqItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faqItem);
        }

        // GET: FaqItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FaqItem == null)
            {
                return NotFound();
            }

            var faqItem = await _context.FaqItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faqItem == null)
            {
                return NotFound();
            }

            return View(faqItem);
        }

        // POST: FaqItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FaqItem == null)
            {
                return Problem("Entity set 'ProjectContext.FaqItem'  is null.");
            }
            var faqItem = await _context.FaqItem.FindAsync(id);
            if (faqItem != null)
            {
                faqItem.IsActive = false;
                _context.Update(faqItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqItemExists(int id)
        {
          return (_context.FaqItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
