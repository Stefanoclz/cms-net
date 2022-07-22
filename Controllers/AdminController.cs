using cms_net.Context;
using cms_net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace cms_net.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ComponentList()
        {
            string path = "C:/Users/Stefano/source/repos/cms-net/Views/Page/Components/";
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

            List<string> files = new List<string>();

            foreach(string dir in dirs)
            {
                string[] divide = dir.Split("/");
                int maxLength = divide.Length;
                files.Add(divide[maxLength - 1]);

            }

            ViewData["ComponentList"] = files;

            return View();
        }

        public IActionResult InstallComponent(string name)
        {
            using(CMSContext db = new CMSContext())
            {
                ComponentDefinition install = new ComponentDefinition() { Key = name };
                db.ComponentDefinitions.Add(install);
                db.SaveChanges();
            }
            return View("ComponentList");
        }

        public IActionResult UninstallComponent(string name)
        {
            using (CMSContext db = new CMSContext())
            {
                ComponentDefinition toUninstall = db.ComponentDefinitions.Where(comp => comp.Key == name).First();
                if(toUninstall != null)
                {
                    db.ComponentDefinitions.Remove(toUninstall);
                    db.SaveChanges();
                }
                else
                {
                    return View("Index");
                }
            }
            return View("ComponentList");
        }
    }
}
