(function ($) {
   $.validator.addMethod('cpfcnpj-or-optional', function (value, element) {
      if (!value) return true;
      return validateCPF(value) || validateCNPJ(value);
   });
   $.validator.unobtrusive.adapters.addBool('cpfcnpj-or-optional');
})(jQuery);