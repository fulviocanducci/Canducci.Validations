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
    public class CpfOrOptionAttribute : ValidationAttribute, IClientModelValidator
#else
    public class CpfOrOptionAttribute : ValidationAttribute
#endif
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            return Validates.ValidateCpf(value.ToString())
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ?? "Cpf inválid");
        }
#if NET6_0_OR_GREATER
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-cpf-or-optional", ErrorMessage ?? "Cpf inválid");
        }
#endif
    }
}
