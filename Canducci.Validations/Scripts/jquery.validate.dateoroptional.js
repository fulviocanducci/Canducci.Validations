(function ($) {
   $.validator.addMethod('date-or-optional', function (value, element, formats) {
      if (!value) return true;
      const formatsArray = formats ? formats.split(',') : ["DD/MM/YYYY", "YYYY-MM-DD"];
      let isValid = false;
      for (let i = 0; i < formatsArray.length; i++) {
         if (dayjs(value, formatsArray[i], true).isValid()) {
            isValid = true;
            break;
         }
      }
      return isValid;
   });
   $.validator.unobtrusive.adapters.addSingleVal('date-or-optional', 'formats');
})(jQuery);
