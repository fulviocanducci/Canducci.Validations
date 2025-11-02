(function ($) {
   $.validator.addMethod('datetime-or-optional', function (value, element, formats) {
      if (!value) return true;
      const formatsArray = formats ? formats.split(',') : [
         "DD/MM/YYYY", "DD/MM/YYYY HH:mm", "DD/MM/YYYY HH:mm:ss",
         "YYYY-MM-DD", "YYYY-MM-DD HH:mm", "YYYY-MM-DD HH:mm:ss"
      ];
      let isValid = false;
      for (let i = 0; i < formatsArray.length; i++) {
         if (dayjs(value, formatsArray[i], true).isValid()) {
            isValid = true;
            break;
         }
      }
      return isValid;
   });
   $.validator.unobtrusive.adapters.addSingleVal('datetime-or-optional', 'formats');
})(jQuery);
