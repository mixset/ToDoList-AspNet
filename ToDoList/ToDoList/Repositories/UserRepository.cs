using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.DAL;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class UserRepository
    {
        private ToDoContext db = new ToDoContext();

        public List<User> getUserData()
        {
            // return db.User.Where(u => u.Id == 1).ToList();
            /*
            var x = db.User
                .Join(db.Role,
                    x => x.Role_id,
                    y => y.Id,
                    (x, y) => new
                    {
                        Id = x.Id,
                        Login = x.Login,
                        Email = x.Email,
                        Created_at = x.Created_at,
                        Label = y.Label,
                    })
                    .Where(user => user.Id == 1).ToList();*/

            var gowno = from user in db.User.Include("Role")
                   where user.Id == 1
                   select user;

            return gowno.ToList();
        }
    }
}