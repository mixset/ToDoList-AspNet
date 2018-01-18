using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace ToDoList.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole login jest wymagane.")]
        [StringLength(100)]
        [Display(Name = "Nazwa użytkownika")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Pole hasło jest wymagane.")]
        [StringLength(150)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole E-mail jest wymagane.")]
        [StringLength(200)]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Wprowadz poprawny adres E-mail.")]
        public string Email { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Data utworzenia konta")]
        public DateTime Created_at { get; set; } = DateTime.Now;

        [DefaultValue(0)]
        [Display(Name = "Status")]
        public int Status { get; set; }

        [Display(Name = "Rola")]
        public int Role_id { get; set; } = 2;

        [ForeignKey("Role_id")]
        public virtual Role Role { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}