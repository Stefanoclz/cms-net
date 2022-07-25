using cms_net.Context;
using cms_net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace cms_net.Controllers
{
    public class ComponentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id, string key, IFormCollection collection)

        {
            Component component = new Component();
            component.PageId = id;

            List<Field> input = new List<Field>();

            string path = $"C:/Users/Stefano/source/repos/cms-net/Views/Page/Components/{key}/data.csv";
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        Field newField = new Field(field);
                        input.Add(newField);
                    }
                }

                component.Fields = input;
                component.Id = id;
                component.Key = key;
                

                return View(component);
            }
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
