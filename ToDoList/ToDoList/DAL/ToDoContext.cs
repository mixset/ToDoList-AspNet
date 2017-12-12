﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("ToDoContext")
        {
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ToDo> ToDo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Turn off pluralization of table's name
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}