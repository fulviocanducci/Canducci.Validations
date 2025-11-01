using System;
using System.ComponentModel.DataAnnotations;
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
        }
#endif
    }
}
