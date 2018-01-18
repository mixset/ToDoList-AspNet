using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;
using ToDoList.Utils.Attributes;

namespace ToDoList.Controllers
{
    public class AdminController : Controller
    {
        private ToDoContext db = new ToDoContext();

        [AppAuthorize("user", "admin")]
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.Role);
            return View(user.ToList());
        }

        [AppAuthorize("user", "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [AppAuthorize("user", "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Role_id = new SelectList(db.Role, "Id", "Label", user.Role_id);
            ViewBag.Status = new SelectList(db.User, "Id", "Status", user.Status);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize("user", "admin")]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,Email,Created_at,Status,Role_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();


                ViewBag.Message = "Dane użytkownika zostały zmienione.";
                ViewBag.Status = "success";

                return RedirectToAction("Index");
            }
            ViewBag.Role_id = new SelectList(db.Role, "Id", "Name", user.Role_id);
            return View(user);
        }


        public ActionResult Delete(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
     
            Session["Message"] = "Użytkownik został usunięty poprawnie.";
            Session["Status"] = "success";

            return RedirectToAction("Index");
        }
    }
}
