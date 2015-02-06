
using System.Collections.Generic;
using System.Linq;

using HtmlAgilityPack;

using Weather.Common.Exceptions;

namespace Weather.Parser.Extensions
{
    public static class HtmlDocumentExtension
    {
        public static string GetInnerText(this HtmlDocument htmlDocument, string xPath)
        {
            return GetNodes(htmlDocument, xPath).First().InnerText;
        }

        public static string GetAttribute(this HtmlDocument htmlDocument, string xPath, string attributeName)
        {
            var attribute = GetNodes(htmlDocument, xPath).First().Attributes.FirstOrDefault(i => i.Name == attributeName);

            if (attribute == null)
            {
                throw new HtmlDocumentNotExistAttributeException(xPath, attributeName);
            }

            return attribute.Value;
        }

        public static bool IsInnerText(this HtmlDocument htmlDocument, string xPath)
        {
            var node = htmlDocument.DocumentNode.SelectNodes(xPath);

            return node != null;
        }

        public static bool IsAttribute(this HtmlDocument htmlDocument, string xPath, string attributeName)
        {
            var node = GetNodes(htmlDocument, xPath).First().Attributes.FirstOrDefault(i => i.Name == attributeName);

            return node != null;
        }

        private static IEnumerable<HtmlNode> GetNodes(HtmlDocument htmlDocument, string xPath)
        {
            var nodes = htmlDocument.DocumentNode.SelectNodes(xPath);

            if (nodes == null)
            {
                throw new HtmlDocumentNotExistXPathException(xPath);
            }

            return nodes;
        }
    }
}
