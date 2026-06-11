using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Canducci.Validations.Attributes
{
    public class PhoneOrOptionalAttribute : ValidationAttribute, IClientModelValidator
    {
        public string Formats { get; }

        public PhoneOrOptionalAttribute(string formats = "10,11")
        {
            Formats = formats;
            ErrorMessage = "Telefone inválido.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            string text = value.ToString();
            if (string.IsNullOrWhiteSpace(text))
            {
                return ValidationResult.Success;
            }
            string numbers = new string(text.Where(char.IsDigit).ToArray());
            IEnumerable<int> formatsArray = Formats.Split(',').Select(x => int.Parse(x.Trim()));
            if (formatsArray.Contains(numbers.Length))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-phone-or-optional", ErrorMessage);
            MergeAttribute(context.Attributes, "data-val-phone-or-optional-formats", Formats);
        }

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
