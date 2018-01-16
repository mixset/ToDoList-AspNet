using System;
using System.Web;
using ToDoList.Controllers;

namespace ToDoList.Attributes
{
    public class RoleAttribute : Attribute
    {
        public RoleAttribute(string Name)
        {
            int user_id = (int) HttpContext.Current.Session["Id"];

            AuthController auth = new AuthController();
            string RoleName = auth.GetUserRole(user_id);

            if (RoleName != Name)
            {
                auth.LogOut();
            }
        }
    }
}