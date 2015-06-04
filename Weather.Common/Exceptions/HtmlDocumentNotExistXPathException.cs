
using System;

using Weather.Utilities.Extensions;

namespace Weather.Common.Exceptions
{
    public class HtmlDocumentNotExistXPathException : Exception
    {
        public HtmlDocumentNotExistXPathException()
        {
        }

        public HtmlDocumentNotExistXPathException(string xPath)
            : base("XPath: {0}".F(xPath))
        {
        }
    }
}
