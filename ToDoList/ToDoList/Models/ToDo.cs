using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole Tytuł jest wymagane.")]
        [StringLength(100)]
        public string Label { get; set; }

        [Required(ErrorMessage = "Pole opis jest wymagane.")]
        [StringLength(200)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole data rozpoczęcia jest wymagane")]
        public DateTime Start_at { get; set; }

        [Required(ErrorMessage = "Pole data zakończenia jest wymagane")]
        public DateTime End_at { get; set; }

        public DateTime Created_at { get; set; }

        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public virtual User User { get; set; }
    }
}