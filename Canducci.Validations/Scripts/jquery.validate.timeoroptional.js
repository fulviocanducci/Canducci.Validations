(function ($) {
   $.validator.addMethod('time-or-optional', function (value, element, formats) {
      if (!value) return true;
      const formatsArray = formats ? formats.split(',') : ["HH:mm", "HH:mm:ss"];
      let isValid = false;
      for (let i = 0; i < formatsArray.length; i++) {
         if (dayjs(value, formatsArray[i], true).isValid()) {
            isValid = true;
            break;
         }
      }
      return isValid;
   });
   $.validator.unobtrusive.adapters.addSingleVal('time-or-optional', 'formats');
})(jQuery);