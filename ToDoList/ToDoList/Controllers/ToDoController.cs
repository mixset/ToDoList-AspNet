using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList.Attributes;
using ToDoList.DAL;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private ToDoContext db = new ToDoContext();

        public ActionResult Index()
        {
            int user_id = (int) Session["Id"];

            var data = from t in db.ToDo
                       join u in db.User on t.User_id equals u.Id 
                       where t.User_id == user_id
                       select t;

            List<ToDo> list = data.ToList();

            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ToDo data)
        {
            if (ModelState.IsValid)
            {
                data.Created_at = DateTime.Now;
                data.User_id = (int)Session["Id"];
                db.ToDo.Add(data);
                db.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = "Notatka została dodana poprawnie.";
                ViewBag.Status = "success";
            }

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ToDo toDo = db.ToDo.Find(id);

            if (toDo == null)
            {
                return HttpNotFound();
            }

            return View(toDo);
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Label,Description,Start_at,End_at")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.User, "Id", "Login", toDo.User_id);
            return View(toDo);
        }
        */

        // GET: ToDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ToDo toDo = db.ToDo.Find(id);

            if (toDo == null)
            {
                return HttpNotFound();
            }

            return View(toDo);
        }
  
        public ActionResult Delete(int id)
        {
            ToDo toDo = db.ToDo.Find(id);
            db.ToDo.Remove(toDo);
            db.SaveChanges();

            Session["Message"] = "Notatka została usunięta poprawnie.";
            Session["Status"] = "success";

            return RedirectToAction("Index");
        }
    }
}