using Microsoft.AspNetCore.Mvc;

namespace cms_net.Controllers
{
    public class ComponentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
