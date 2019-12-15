using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthwindStore.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "northwind-id")]
    public class NorthwindImageTagHelper : TagHelper
    {
        [HtmlAttributeName("northwind-id")]
        public int NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("href");
            output.Attributes.SetAttribute("href", "images/" + NorthwindId);
        }
    }
}
