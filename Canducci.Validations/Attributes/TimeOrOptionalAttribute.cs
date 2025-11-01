using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
#endif
namespace Canducci.Validations.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
#if NET6_0_OR_GREATER
    public sealed class TimeOrOptionalAttribute : ValidationAttribute, IClientModelValidator
#else
    public sealed class TimeOrOptionalAttribute : ValidationAttribute
#endif
    {
        public string[] Formats { get; }
        public TimeOrOptionalAttribute(params string[] formats)
        {
            Formats = formats.Length > 0 ? formats : new[] { "HH:mm", "HH:mm:ss" };
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
#if NET6_0_OR_GREATER
            if (value is TimeOnly)
            {
                return ValidationResult.Success;
            }
#endif
            if (value is TimeSpan)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Time inválid");
        }
#if NET6_0_OR_GREATER
        public void AddValidation(ClientModelValidationContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-time-or-optional", ErrorMessage ?? "Time inválid");
            context.Attributes.Add("data-val-time-or-optional-formats", string.Join(",", Formats.Select(c => c.Trim())));
        }
#endif
    }
}
