using System;
using ToDoList.Controllers;

namespace ToDoList.Attributes
{
    public class LoggedInAttribute : Attribute
    {
        public LoggedInAttribute()
        {
            if (AuthController.IsLogged() == false)
            {
                AuthController controller = new AuthController();
                controller.RedirectToHome();
            }
        }
    }
}