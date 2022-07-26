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

        public IActionResult Create(int pageId, string key)

        {

            List<string> input = new List<string>();

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
                        input.Add(field);
                    }
                }

                Component component = new Component();
                component.PageId = pageId;
                component.Key = key;
                component.Fields = new List<Field>();
                foreach(string field in input)
                {
                    Field nuovo = new Field(field);
                    component.Fields.Add(nuovo);
                }

                return View(component);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int pageId, string key, IFormCollection formData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            CMSContext db = new CMSContext();

            Component nuovo = new Component();

            nuovo.Key = key;
            nuovo.PageId = pageId;
            nuovo.Fields = new List<Field>();
            foreach (string fld in formData.Keys)
            {
                Field nuovoField = new Field(fld);
                nuovo.Fields.Add(nuovoField);
            }

            db.Components.Add(nuovo);
            db.SaveChanges();

            return RedirectToAction("Index", "Page");
        }
    }
}
