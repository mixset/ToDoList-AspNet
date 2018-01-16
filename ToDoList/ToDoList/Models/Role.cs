using System;

namespace ToDoList.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public DateTime Created_at { get; set; }
    }
}