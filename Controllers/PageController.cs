using cms_net.Context;
using cms_net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cms_net.Controllers
{
    public class PageController : Controller
    {
        private readonly ILogger<PageController> _logger;

        public PageController(ILogger<PageController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using(CMSContext db = new CMSContext())
            {
                List<Page> pages = db.Pages.ToList();
                return View(pages);
            }
 
        }

        public IActionResult Detail(int pageId)
        {
            

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            Page newPage = new Page();

            return View(newPage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Page page)
        {
           
            if (!ModelState.IsValid)
            {
                return View(page);
            }

            using(CMSContext db = new CMSContext())
            {
                db.Pages.Add(page);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int pageId)
        {

            using(CMSContext db = new CMSContext())
            {
                Page find = db.Pages.Where(find => find.Id == pageId).FirstOrDefault();
                if(find != null)
                {
                    return View(find);
                }
                else
                {
                    return View();
                }
                
            }
     
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Page page)
        {
            using (CMSContext db = new CMSContext())
            {
                Page find = db.Pages.Where(p => p.Id == page.Id).FirstOrDefault();
                if (find == null)
                {
                    return View(page);
                }
                else
                {
                    find.Title = page.Title;
                    db.Pages.Update(find);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

        }


        public IActionResult Installed(int pageId)
        {
            using (CMSContext db = new CMSContext())
            {
                List<ComponentDefinition> compList = db.ComponentDefinitions.ToList();
                List<int> found = new List<int>();
                found.Add(pageId);
                ViewData["id"] = found;
                return View(compList);
            }

        }



        public IActionResult Privacy()
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