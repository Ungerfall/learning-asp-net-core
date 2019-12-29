using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindStore.Extensions
{
	public static class HtmlHelperExtensions
	{
		public static IHtmlContent NorthwindImageLink<TModel>(this IHtmlHelper<TModel> html, int imageId, string anchorText)
		{
			return new HtmlString($"<a href=\"/images/{imageId}\">{anchorText}</a>");
		}

	}
}
