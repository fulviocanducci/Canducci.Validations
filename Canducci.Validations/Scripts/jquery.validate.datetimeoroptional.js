(function ($) {
   $.validator.addMethod('datetime-or-optional', function (value, element, formats) {
      if (!value) {
         return true;
      }
      const formatsArray = formats ? formats.split(',') : [
         "DD/MM/YYYY", "DD/MM/YYYY HH:mm", "DD/MM/YYYY HH:mm:ss",
         "YYYY-MM-DD", "YYYY-MM-DD HH:mm", "YYYY-MM-DD HH:mm:ss"
      ];
      return dayjs(value, formatsArray, true).isValid();
   });
   $.validator.unobtrusive.adapters.addSingleVal('datetime-or-optional', 'formats');
})(jQuery);
