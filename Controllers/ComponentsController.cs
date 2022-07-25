using cms_net.Context;
using cms_net.Models;
using Microsoft.AspNetCore.Mvc;

namespace cms_net.Controllers
{
    public class ComponentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int pageId)
        {
            CMSContext db = new CMSContext();

            ViewData["pageId"] = pageId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Component component)
        {
            CMSContext db = new CMSContext();

            db.Components.Add(component);

            return RedirectToAction("Index", "PageController", new {pageId = component.PageId});
        }
    }
}
