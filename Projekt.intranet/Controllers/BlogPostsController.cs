
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;
using Projekt.Data.Data.Sharded;

namespace Projekt.intranet.Controllers
{
    public class BlogPostsController : BaseController<BlogPost>
    {
        public BlogPostsController(ProjectContext context) : base(context)
        {
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BlogPost == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .Include(b => b.HeaderImage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BlogPost == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            ViewData["HeaderImageId"] = new SelectList(_context.Set<Picture>(), "Id", "ImageData", blogPost.HeaderImageId);
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Content,HeaderImageId,MetaTitle,MetaDescription,Id,Name,Description,IsActive,LastModificationDate,LastModifiedBy,CreationDate,CreatedBy")] BlogPost blogPost)
        {
            blogPost.LastModificationDate = DateTime.Now;
            blogPost.LastModifiedBy = 1;
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
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
            ViewData["HeaderImageId"] = new SelectList(_context.Set<Picture>(), "Id", "ImageData", blogPost.HeaderImageId);
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BlogPost == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .Include(b => b.HeaderImage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BlogPost == null)
            {
                return Problem("Entity set 'ProjectContext.BlogPost'  is null.");
            }
            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost != null)
            {
                blogPost.IsActive = false;
                _context.Update(blogPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return (_context.BlogPost?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public override async Task SetSelectList()
        {

            var pictures = await _context.Picture.ToListAsync();
            ViewBag.Pictures = new SelectList(pictures, "HeaderImageId", "Nazwa");
        }

        public override async Task<List<BlogPost>> getEntityList()
        {
            return await (
                          from blogPost in _context.BlogPost
                          where blogPost.IsActive == true
                          select blogPost
                  ).ToListAsync();
        }
    }
}
