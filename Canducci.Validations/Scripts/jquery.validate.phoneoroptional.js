(function ($) {
    $.validator.addMethod("phone-or-optional", function (value, element, formats) {
        if (!value) {
            return true;
        }
        const text = value.replace(/\D/g, "");
        const formatsArray = formats ? formats.split(",") : ["10", "11"];
        return formatsArray.some((x) => text.length === parseInt(x, 10));
    });
    $.validator.unobtrusive.adapters.addSingleVal("phone-or-optional", "formats");
})(jQuery);