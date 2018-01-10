using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.ViewModel
{
    public class UserViewModel
    {
        public IEnumerable<User> User { get; set; }
    }
}