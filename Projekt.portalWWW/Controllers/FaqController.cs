using Microsoft.AspNetCore.Mvc;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;
using Projekt.portalWWW.Models;
using System.Diagnostics;

namespace Projekt.portalWWW.Controllers
{
    public class FaqController : BaseController
    {
        public FaqController(ProjectContext context) : base(context) { }

        public async override Task<IActionResult> Index(int id)
        {
            prepareLayoutData();
            return View((
                    from FaqItem in _context.FaqItem
                    select FaqItem
                ).ToList());
        }
    }
}