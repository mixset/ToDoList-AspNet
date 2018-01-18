using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Models;
using ToDoList.Utils.Attributes;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private ToDoContext db = new ToDoContext();

        [AppAuthorize("user")]
        public ActionResult Index()
        {
            int user_id = (int) Session["Id"];

            var data = from t in db.ToDo
                       join u in db.User on t.User_id equals u.Id 
                       where t.User_id == user_id
                       select t;

            createJSONFile(user_id);

            List<ToDo> list = data.ToList();

            return View(list);
        }
        
        private void createJSONFile(int user_id)
        {
           string json = string.Empty;
            json = "{\"monthly\": ";

            List<object> objects = new List<object>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ToDoContext"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    List<string> columns = new List<string>
                    {
                        "id",
                        "name",
                        "startdate",
                        "enddate",
                        "color",
                        "url"
                    };

                    command.CommandText = "SELECT Id, Label, Start_at, End_at FROM dbo.ToDo WHERE User_id = " + user_id;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IDictionary<string, object> record = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                record.Add(columns.ElementAt(i), reader[i]);
                            }

                            record.Add(columns.ElementAt(4), "#FFB128");
                            record.Add(columns.ElementAt(5), "");

                            objects.Add(record);
                        }
                    }
                }
            }

            json += JsonConvert.SerializeObject(objects); 
            json += "  }";

            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "event.json"))
            {
                sw.Write(json);
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize("user")]
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

        [AppAuthorize("user")]
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
 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize("user")]
        public ActionResult Edit([Bind(Include = "Id,Label,Description,")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();

                Session["Message"] = "Zmiany w notatce zostały zapisane poprawnie..";
                Session["Status"] = "success";

                return RedirectToAction("Index");
            }

            ViewBag.User_id = Session["Id"];

            return View(toDo);
        }

        [AppAuthorize("user")]
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

        [AppAuthorize("user")]
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