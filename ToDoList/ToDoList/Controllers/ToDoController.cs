using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        public ActionResult Index()
        {
            if (AuthController.IsLogged() == false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}