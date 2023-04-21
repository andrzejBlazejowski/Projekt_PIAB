using Microsoft.AspNetCore.Mvc;
using Projekt.Data.Data;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class FaqItemController : BaseController
    {
        public FaqItemController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();
            return View();
        }
    }
}