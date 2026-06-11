(function ($) {
    $.validator.addMethod("money", function (value, element, params) {
        if (!value) return true;
        var decimalSeparator = params.decimal || ",";
        var thousandSeparator = params.thousand || ".";
        value = value.split(thousandSeparator).join("");
        value = value.replace(decimalSeparator, ".");
        return !isNaN(parseFloat(value));
    });

    $.validator.unobtrusive.adapters.add("money", ["decimal", "thousand"], function (options) {
        options.rules["money"] = {
            decimal: options.params.decimal,
            thousand: options.params.thousand,
        };
        options.messages["money"] = options.message;
    });
})(jQuery);