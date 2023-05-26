using Microsoft.AspNetCore.Mvc;
using Projekt.Data.Data;
using Projekt.Data.Data.Sharded;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();

            //TODO: Find out why linQ does not searches for image itself - mostlikelly enumerations error
            var PromotedProducts = _context.Product.OrderByDescending(p => p.CreationDate).Take(2).ToList();

            foreach (var product in PromotedProducts)
            {
                Picture Image =  _context.Picture.FirstOrDefault(p => p.Id == product.ImageId);
                product.Image = Image;
            }

            ViewBag.PromotedProducts = PromotedProducts;
            var LatestBlogPost = _context.BlogPost.OrderByDescending(p => p.CreationDate).Take(1).ToList();

            foreach (var blogPost in LatestBlogPost)
            {
                Picture Image = _context.Picture.FirstOrDefault(p => p.Id == blogPost.HeaderImageId);
                blogPost.HeaderImage = Image;
            }
            ViewBag.LatestBlogPost = LatestBlogPost;
            ViewBag.PromotedProducts = PromotedProducts;
            return View();
        }
    }
}