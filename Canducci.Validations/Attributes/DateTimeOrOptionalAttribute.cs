using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
#if NET5_0_OR_GREATER
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
#endif
namespace Canducci.Validations.Attributes
{
    [AttributeUsage(AttributeTargets.Property)] 
#if NET6_0_OR_GREATER
    public sealed class DateTimeOrOptionalAttribute : ValidationAttribute, IClientModelValidator
#else
    public sealed class DateTimeOrOptionalAttribute : ValidationAttribute
#endif
    {
        public string[] Formats { get; }
        public DateTimeOrOptionalAttribute(params string[] formats)
        {
            Formats = formats.Length > 0
                ? formats
                : new[] { "DD/MM/YYYY", "DD/MM/YYYY HH:mm", "DD/MM/YYYY HH:mm:ss", "YYYY-MM-DD", "YYYY-MM-DD HH:mm", "YYYY-MM-DD HH:mm:ss" };
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (value is DateTime)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "DateTime inválid");
        }
#if NET6_0_OR_GREATER
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-datetime-or-optional", ErrorMessage ?? "DateTime inválid");
            context.Attributes.Add("data-val-datetime-or-optional-formats", string.Join(",", Formats.Select(c =>c.Trim())));
        }
#endif
    }
}
