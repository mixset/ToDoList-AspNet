using System.Web;
using System.Web.Mvc;
using ToDoList.Controllers;
using ToDoList.DAL;

namespace ToDoList.Utils.Attributes
{
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        private ToDoContext db = new ToDoContext();
        private readonly string[] allowedroles;

        public AppAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            AuthController auth = new AuthController();
            int user_id = (int)HttpContext.Current.Session["Id"];

            foreach (var role in allowedroles)
            {
                var user = auth.GetUserRole(user_id);
                if (user.Name.Equals(role))
                {
                    authorize = true; 
                }
            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}