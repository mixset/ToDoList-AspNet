using System;
using System.Collections.Generic;
using System.Data.Entity;
using ToDoList.Models;
using ToDoList.Migrations;
using System.Data.Entity.Migrations;

namespace ToDoList.DAL
{
    public class ToDoInitializer : MigrateDatabaseToLatestVersion<ToDoContext, Configuration>
    {
        public static void SeedToDoData(ToDoContext context)
        {
            var roles = new List<Role>
            {
                new Role() { Id = 1, Name = "admin", Label = "Administrator", Created_at = DateTime.Now },
                // new Role() { Name = "moderator", Label = "Moderator", Created_at = DateTime.Now };
                new Role() { Id = 2, Name = "user", Label = "Użytkownik", Created_at = DateTime.Now }
            };

            roles.ForEach(k => context.Role.AddOrUpdate(k));
            context.SaveChanges();

            var users = new List<User>
            {
                new User() {Id = 1, Login = "Franek", Password = "password" , Email = "franek@gmail.com", Created_at = DateTime.Now, Status = 1 , Role_id = 1},
                new User() {Id = 2, Login = "Maciek", Password = "password", Email = "maciek@gmail.com", Created_at = DateTime.Now, Status = 0, Role_id = 2},
                new User() {Id = 3, Login = "Kamil", Password = "password", Email = "kamil@gmail.com", Created_at = DateTime.Now, Status = 0, Role_id = 2},
            };

            users.ForEach(k => context.User.AddOrUpdate(k));
            context.SaveChanges();

            var todos = new List<ToDo>
            {
                new ToDo() { Id = 1, Label = "Tytuł pierwszej notatki", Description = "Bardzo ważna treść w pierwszej notatce", Start_at = DateTime.Now, End_at = DateTime.Now.AddDays(3), Created_at = DateTime.Now, User_id = 2},
                new ToDo() { Id = 1, Label = "Zdać ten rok", Description = "Druga niesamowita notatka", Start_at = DateTime.Now, End_at = DateTime.Now.AddDays(1), Created_at = DateTime.Now, User_id = 3},
            };

            todos.ForEach(k => context.ToDo.AddOrUpdate(k));
            context.SaveChanges();

        }
    }
}