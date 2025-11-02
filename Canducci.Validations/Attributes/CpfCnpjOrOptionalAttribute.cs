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
    public class CpfCnpjOrOptionalAttribute : ValidationAttribute, IClientModelValidator
#else
    public class CpfCnpjOrOptionalAttribute : ValidationAttribute
#endif
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            var input = value.ToString();
            if (Validates.ValidateCpf(input) || Validates.ValidateCnpj(input))
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? "CPF ou CNPJ invalid");
        }

#if NET6_0_OR_GREATER
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-cpfcnpj-or-optional", ErrorMessage ?? "CPF ou CNPJ invalid");
        }
#endif
    }
}