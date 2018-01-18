using System.Linq;
using System.Web.Mvc;
using ToDoList.DAL;
using ToDoList.Utils.Attributes;

namespace ToDoList.Controllers
{
    public class RoleController : Controller
    {
        private ToDoContext db = new ToDoContext();

        [AppAuthorize("admin")]
        public ActionResult Index()
        {
            return View(db.Role.ToList());
        }
    }
}
