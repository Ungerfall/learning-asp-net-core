using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NorthwindStore.Data.Models.Validation
{
    public class AsciiCharacters : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is string text))
                return base.IsValid(value, validationContext);

            return text.Any(x => x < 0 || x >= 128)
                ? new ValidationResult(ErrorMessage)
                : ValidationResult.Success;
        }

        public new string ErrorMessage => "Name should contain only ASCII characters";
    }
}
