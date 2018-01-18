using System.Collections.Generic;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<string> slides = new List<string>();
            slides.Add("Slides/slide_1.jpg");
            slides.Add("Slides/slide_2.jpg");
            slides.Add("Slides/slide_3.jpg");

            ViewData["Slides"] = slides;

            return View();
        }
    }
}