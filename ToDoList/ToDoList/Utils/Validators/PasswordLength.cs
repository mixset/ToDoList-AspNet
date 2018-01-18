using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Utils.Validators
{
    public class PasswordLength : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " jest wymagane.");
            }

            return value.ToString().Length < 6 ?
                ValidationResult.Success
                : new ValidationResult(validationContext.DisplayName + " musi zawierac co najmniej 5 znakow.");
        }
    }
}