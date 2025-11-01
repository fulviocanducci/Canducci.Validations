(function ($) {
   $.validator.addMethod('time-or-optional', function (value, element, formats) {
      if (!value) {
         return true;
      }
      const formatsArray = formats ? formats.split(',') : ["HH:mm", "HH:mm:ss"];
      return dayjs(value, formatsArray, true).isValid();
   });
   $.validator.unobtrusive.adapters.addSingleVal('time-or-optional', 'formats');
})(jQuery);

