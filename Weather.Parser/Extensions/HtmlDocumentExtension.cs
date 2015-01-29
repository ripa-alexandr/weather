
using System.Linq;

using HtmlAgilityPack;

namespace Weather.Parser.Extensions
{
    public static class HtmlDocumentExtension
    {
        public static string GetInnerText(this HtmlDocument htmlDocument, string xPath)
        {
            var node = htmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();
            
            return node == null ? string.Empty : node.InnerText;
        }

        public static string GetAttribute(this HtmlDocument htmlDocument, string xPath, string attribute)
        {
            var node = htmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();

            return node == null ? string.Empty : node.Attributes[attribute].Value;
        }

        public static bool IsInnerText(this HtmlDocument htmlDocument, string xPath)
        {
            var node = htmlDocument.DocumentNode.SelectNodes(xPath);

            return node != null;
        }

        public static bool IsAttribute(this HtmlDocument htmlDocument, string xPath, string attribute)
        {
            var node = htmlDocument.DocumentNode.SelectNodes(xPath).First().Attributes.FirstOrDefault(i => i.Name == attribute);

            return node != null;
        }
    }
}
