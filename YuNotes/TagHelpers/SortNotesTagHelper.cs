using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace YuNotes.TagHelpers
{
    public class SortNotesTagHelper : TagHelper
    {
        public SortState Property { get; set; }
        public string? Action { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;

        IUrlHelperFactory urlHelperFactory;
        public SortNotesTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";

            Guid.TryParse(ViewContext.HttpContext.Request.Query["groupid"], out Guid groupId);
            string? sortTitle = ViewContext.HttpContext.Request.Query["title"];

            string? url;
            if (groupId != Guid.Empty)
            {
                url = urlHelper.Action(Action, new { sortOrder = Property, groupId = groupId, title = sortTitle });
            }
            else
            {
                url = urlHelper.Action(Action, new { sortOrder = Property, title = sortTitle });
            }
            output.Attributes.SetAttribute("href", url);    
        }
    }
}
