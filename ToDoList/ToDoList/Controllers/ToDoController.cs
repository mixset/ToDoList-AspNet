using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private ToDoContext db = new ToDoContext();

        public ActionResult Index()
        {
            if (AuthController.IsLogged() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var data = from todo in db.ToDo
                       where todo.User_id == (int)Session["Id"]
                       select todo;

            ViewBag.Tasks = data;

            return View();
        }

        public ActionResult Add()
        {
            if (AuthController.IsLogged() == false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Add(ToDo data)
        {
            if (ModelState.IsValid)
            {
                data.Created_at = DateTime.Now;
                data.User_id = (int) Session["Id"];
                db.ToDo.Add(data);
                db.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = "Notatka została dodana poprawnie.";
                ViewBag.Status = "success";
            }

            return View();
        }

        private IEnumerable<ToDo> getTasks()
        {
            var data = from todo in db.ToDo
                         where todo.User_id == (int)Session["Id"]
                         select todo;

            return data;
        }
    }
}