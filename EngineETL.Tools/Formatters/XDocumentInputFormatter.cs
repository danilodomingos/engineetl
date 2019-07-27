using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EngineETL.Tools.Formatters
{
    public class XDocumentInputFormatter : InputFormatter
    {
        public XDocumentInputFormatter()
        {
            SupportedMediaTypes.Add("application/xml");
        }

        protected override bool CanReadType(Type type)
        {
            if (type.IsAssignableFrom(typeof(XDocument))) return true;
            return base.CanReadType(type);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var xmlDoc = await XDocument.LoadAsync(context.HttpContext.Request.Body, LoadOptions.None, CancellationToken.None);

            return InputFormatterResult.Success(xmlDoc);
        }
    }
}
