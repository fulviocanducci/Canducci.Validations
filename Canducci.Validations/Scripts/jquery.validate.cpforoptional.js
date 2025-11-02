(function ($) {
   $.validator.addMethod('cpf-or-optional', function (value, element) {
      if (!value) return true;
      return validateCPF(value);
   });
   $.validator.unobtrusive.adapters.addBool('cpf-or-optional');
})(jQuery);