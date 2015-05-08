using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Weather.Website.Extensions
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString Td<T>(this HtmlHelper htmlHelper, IEnumerable<T> items)
        {
            var result = new StringBuilder();
            var tags = items.Select(i => new TagBuilder("td") { InnerHtml = i.ToString() });

            foreach (var tag in tags)
            {
                result.AppendLine(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}