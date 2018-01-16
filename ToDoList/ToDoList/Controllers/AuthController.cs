using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void RedirectToHome()
        {
            RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(User data)
        {
            if (ModelState.IsValid) { 
                db.User.Add(data);
                db.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = data.Login + " został poprawnie zarejstrowany!";
                ViewBag.Status = "success";
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

            if (usr != null) {
                Session["Id"] = usr.Id;
                Session["Login"] = usr.Login.ToString();
                return RedirectToAction("Index", "ToDo");
            } else {
                ModelState.AddModelError("", "Login lub hasło są niepoprawne.");
            }
            
            return View();
        }

        public static bool IsLogged()
        {
            return System.Web.HttpContext.Current.Session["Id"] != null && System.Web.HttpContext.Current.Session["Login"] != null;
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public string GetUserRole(int User_id)
        {
            var data = db.User.Where(u => u.Id == User_id).Include("Role").FirstOrDefault();
            return data.Role.Name;
        }
    }
}