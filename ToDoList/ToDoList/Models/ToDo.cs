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
        [Display(Name = "Tytuł")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Pole opis jest wymagane.")]
        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        [Display(Name = "Opis zadania")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole data rozpoczęcia jest wymagane")]
        [Display(Name = "Data rozpoczęcia zadania")]
        public DateTime Start_at { get; set; }

        [Required(ErrorMessage = "Pole data zakończenia jest wymagane")]
        [Display(Name = "Data zakończenia zadania")]
        public DateTime End_at { get; set; }

        [Display(Name = "Data utworzenia zadania")]
        public DateTime Created_at { get; set; }

        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public virtual User User { get; set; }
    }
}