using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.CMS;

public class SiteParameter : Controller
{
    private readonly ProjectContext _context;

    public SiteParameter(ProjectContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> SiteParameterView(string key)
    {
        Parameter? model = await _context.Parameter.FindAsync(key);
        return PartialView("SiteParameterView", model.Value);
    }
}