using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return await _context.Product.Include(p => p.Image).Include(p => p.ProductCategory).ToListAsync();
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

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Picture, "Id", "ImageData", product.ImageId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategory, "Id", "LinkTitle", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Content,Price,VatRate,CountInWearhouse,IsVisible,BrandName,ImageId,ProductCategoryId,MetaTitle,MetaDescription,Id,Name,Description,IsActive,LastModificationDate,LastModifiedBy,CreationDate,CreatedBy")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ImageId"] = new SelectList(_context.Picture, "Id", "ImageData", product.ImageId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategory, "Id", "LinkTitle", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Image)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ProjectContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
