using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;
using ToDoList.Utils.Attributes;

namespace ToDoList.Controllers
{
    public class UserController : Controller
    {
        private ToDoContext db = new ToDoContext();

        [AppAuthorize("user", "admin")]
        public ActionResult Details()
        {
            int user_id = (int)Session["Id"];

            var data = from user in db.User.Include("Role")
                        where user.Id == user_id
                        select user;

            return View(data.ToList());
        }

        [AppAuthorize("user", "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User toDo = db.User.Find(id);

            if (toDo == null)
            {
                return HttpNotFound();
            }

            return View(toDo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize("user", "admin")]
        public ActionResult Edit([Bind(Include = "Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Edycja użytkownika zakończyła się sukcesem.";
            ViewBag.Status = "success";

            return View(user);
        }
    }
}