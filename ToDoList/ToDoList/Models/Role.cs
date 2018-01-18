using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa robocza")]
        public string Name { get; set; }

        [Display(Name = "Nazwa roli")]
        public string Label { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime Created_at { get; set; }
    }
}