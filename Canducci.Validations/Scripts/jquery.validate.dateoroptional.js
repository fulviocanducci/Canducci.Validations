(function ($) {
   $.validator.addMethod('date-or-optional', function (value, element, formats) {
      if (!value) {
         return true;
      }
      const formatsArray = formats ? formats.split(',') : ["DD/MM/YYYY", "YYYY-MM-DD"];
      return dayjs(value, formatsArray, true).isValid();
   });
   $.validator.unobtrusive.adapters.addSingleVal('date-or-optional', 'formats');
})(jQuery);