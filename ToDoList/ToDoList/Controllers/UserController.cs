using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;
using ToDoList.ViewModel;

namespace ToDoList.Controllers
{
    public class UserController : Controller
    {
        private ToDoContext db = new ToDoContext();

        public ActionResult Account()
        {
            if (AuthController.IsLogged() == false)
            {
                return RedirectToAction("Index", "Home");
            }

            var data = db.User.Where(u => u.Id == 1).ToList();

            var resources = new UserViewModel
            {
                User = data
            };

            return View(resources);
        }

        public ActionResult Settings()
        {
            if (AuthController.IsLogged() == false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}