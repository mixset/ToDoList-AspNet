namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL;

    public sealed class Configuration : DbMigrationsConfiguration<ToDoList.DAL.ToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ToDoList.DAL.ToDoContext";
        }

        protected override void Seed(ToDoContext context)
        {
            ToDoInitializer.SeedToDoData(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
