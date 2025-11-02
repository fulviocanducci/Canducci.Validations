using Canducci.Validations.Internals;
using System;
using System.ComponentModel.DataAnnotations;
#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
#endif
namespace Canducci.Validations.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
#if NET6_0_OR_GREATER
    public class CnpjOrOptionalAttribute : ValidationAttribute, IClientModelValidator
#else
    public class CnpjOrOptionalAttribute : ValidationAttribute
#endif
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            return Validates.ValidateCnpj(value.ToString())
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ?? "Cnpj inválid");
        }
#if NET6_0_OR_GREATER
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-cnpj-or-optional", ErrorMessage ?? "Cnpj inválid");
        }
#endif
    }
}
