(function ($) {
   $.validator.addMethod('cnpj-or-optional', function (value, element) {
      if (!value) return true;
      return validateCNPJ(value);
   });
   $.validator.unobtrusive.adapters.addBool('cnpj-or-optional');
})(jQuery);
