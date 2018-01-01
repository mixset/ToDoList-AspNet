using System;
using System.Linq;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class AuthController : Controller
    {
        private ToDoContext db = new ToDoContext();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User data)
        {
            if (ModelState.IsValid) { 
                db.User.Add(data);
                db.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = data.Login + "Successfully Registered";
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User data)
        {       
            var usr = db.User.Where(u => u.Login == data.Login && u.Password == data.Password).FirstOrDefault();
            Console.WriteLine(usr);
            if (usr != null) {
                Session["Id"] = usr.Id.ToString();
                Session["Login"] = usr.Login.ToString();
                return RedirectToAction("LoggedIn");
            } else {
                ModelState.AddModelError("", "Username or Password is wrong.");
            }
            
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["Id"] != null) {
                return View();
            } else {
                return RedirectToAction("Login");
            }
        }
    }
}