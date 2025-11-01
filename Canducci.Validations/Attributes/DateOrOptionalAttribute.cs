using System;
using System.ComponentModel.DataAnnotations;
#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
#endif
namespace Canducci.Validations.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
#if NET6_0_OR_GREATER
    public sealed class DateOrOptionalAttribute : ValidationAttribute, IClientModelValidator
#else    
    public sealed class DateOrOptionalAttribute : ValidationAttribute
#endif
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
#if NET6_0_OR_GREATER
            if (value is DateOnly)
            {
                return ValidationResult.Success;
            }
#endif
            if (value is DateTime)
            {
                return ValidationResult.Success;
            }            
            return new ValidationResult(ErrorMessage ?? "Date inválid");
        }
#if NET6_0_OR_GREATER
        public void AddValidation(ClientModelValidationContext context)
        {            
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-date-or-optional", ErrorMessage ?? "Date inválid");
        }
#endif
    }
}
