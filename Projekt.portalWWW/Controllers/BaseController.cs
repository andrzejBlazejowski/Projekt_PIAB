using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;
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
                    select Site
                ).ToList();
/*
            ViewBag.BottomMenu =
                (
                // TBD
                ).ToList();

            ViewBag.SiteParameters =
                (
                // TBD
                ).ToList();

            ViewBag.socialMedia =
                (
                // TBD
                ).ToList();

            ViewBag.paymentMethods =
                (
                // TBD
                ).ToList();
*/
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
