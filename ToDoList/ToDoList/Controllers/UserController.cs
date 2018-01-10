using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;
using ToDoList.Repositories;

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

            return View(new UserRepository().getUserData());
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