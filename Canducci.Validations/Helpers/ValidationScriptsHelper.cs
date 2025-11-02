#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Canducci.Validations.Helpers
{
    public static class ValidationScriptsHelper
    {
        /// <summary>
        /// Injeta os scripts do pacote de validações (Day.js + custom validations)
        /// </summary>
        public static IHtmlContent AddValidationScripts(this IHtmlHelper html)
        {
            var scripts = @"
<script src=""/lib/canducci-validations/validates.js""></script>
<script src=""/lib/canducci-validations/dayjs.min.js""></script>
<script src=""/lib/canducci-validations/plugin/customParseFormat.js""></script>
<script src=""/lib/canducci-validations/jquery.validate.config.js""></script>
<script src=""/lib/canducci-validations/jquery.validate.dateoroptional.js""></script>
<script src=""/lib/canducci-validations/jquery.validate.datetimeoroptional.js""></script>
<script src=""/lib/canducci-validations/jquery.validate.timeoroptional.js""></script>
<script src=""/lib/canducci-validations/jquery.validate.cpforoptional.js""></script>
<script src=""/lib/canducci-validations/jquery.validate.cnpjoroptional.js""></script>
<script src=""/lib/canducci-validations/jquery.validate.cpfcnpjoroptional.js""></script>";
            return new HtmlString(scripts);
        }
    }
}
#endif
