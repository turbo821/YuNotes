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
        public string? AspRouteGroupid { get; set; }
        public string AspRouteSearch { get; set; }

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

            //Guid.TryParse(ViewContext.HttpContext.Request.Query["groupid"], out Guid groupId);
            //string? searchTitle = ViewContext.HttpContext.Request.Query["searchtitle"];

            string? url = urlHelper.Action(Action);

            output.Attributes.SetAttribute("href", $"{url}?SortOrder={Property}&GroupId={AspRouteGroupid}&SearchTitle={AspRouteSearch}");    
        }
    }
}
