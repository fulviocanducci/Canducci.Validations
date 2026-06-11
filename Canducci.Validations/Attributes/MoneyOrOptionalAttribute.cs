using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Canducci.Validations.Attributes
{
    public class MoneyOrOptionalAttribute : ValidationAttribute, IClientModelValidator
    {
        public string DecimalSeparator { get; }
        public string ThousandSeparator { get; }

        public MoneyOrOptionalAttribute(string decimalSeparator = ".", string thousandSeparator = ",")
        {
            DecimalSeparator = decimalSeparator;
            ThousandSeparator = thousandSeparator;
            ErrorMessage = "Value money inválid.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (value is decimal)
            {
                return ValidationResult.Success;
            }
            if (value is float)
            {
                return ValidationResult.Success;
            }
            if (value is double)
            {
                return ValidationResult.Success;
            }
            string text = value.ToString().Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                return ValidationResult.Success;
            }
            text = text.Replace(ThousandSeparator, "").Replace(DecimalSeparator, ".");
            return decimal
                .TryParse(text, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out _)
                    ? ValidationResult.Success
                    : new ValidationResult(ErrorMessage);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-money", ErrorMessage);
            MergeAttribute(context.Attributes, "data-val-money-decimal", DecimalSeparator);
            MergeAttribute(context.Attributes, "data-val-money-thousand", ThousandSeparator);
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
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
