
using System;

using Weather.Common.Extensions;

namespace Weather.Common.Exceptions
{
    public class HtmlDocumentNotExistAttributeException : Exception
    {
        public HtmlDocumentNotExistAttributeException()
        {
        }

        public HtmlDocumentNotExistAttributeException(string xPath, string attribute)
            : base("XPath: {0}, Attribute: {1}".F(xPath, attribute))
        {
        }
    }
}
