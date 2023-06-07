using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;
using Projekt.Data.Data.Shop;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ProjectContext _context;

        public BaseController(ProjectContext context)
        {
            _context = context;
        }

        protected void prepareLayoutData() {
            ViewBag.Sites =
                (
                    from Site in _context.Site
                    where Site.IsActive
                    select Site
                ).ToList();

            ViewBag.ProductCategories =
                (
                    from ProductCategory in _context.ProductCategory
                    where ProductCategory.IsActive
                    select ProductCategory
                   
                ).ToList();

            ViewBag.SiteParameters = _context.Parameter.Where(p => p.IsActive == true)
                .Select(p => new { p.Key, p.Value })
                .ToDictionary(p => p.Key, p => p.Value);
        }


        public abstract Task<IActionResult> Index(int id);
        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult ShopProduct()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult BlogPost()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
