using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using YuNotes.ViewModels;

namespace YuNotes.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        IUrlHelperFactory urlHelperFactory;
        public string? Action { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;
        public PageViewModel PageModel { get; set; } = null!;
        public string? AspRouteGroupid {  get; set; }
        public string? AspRouteSearch { get; set; }
        public string? AspRouteSort { get; set; }
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            string? url = urlHelper.Action(Action);
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "pag-div");
            string innerContent = "";

                for (int page = 1; page <= PageModel.TotalPages; page++)
                {
                    innerContent += CreateLi(PageModel, url, page);

                }
            output.Content.SetHtmlContent($"<ul class=\"pagination\">{innerContent}</ul>");
        }

        private string CreateLi(PageViewModel? pageModel, string? url, int pageNumber = 1)
        {
            string content = "";
            string itemClass = "page-item ";
            string anchorAttribute = "";
            if (pageNumber == pageModel?.PageNumber)
            {
                itemClass += "active";
            }
            else
            {
                anchorAttribute = $"href=\"{url}?Page={pageNumber}&SortOrder={AspRouteSort}&GroupId={AspRouteGroupid}&SearchTitle={AspRouteSearch}\"";
            }
            content = $@"<li class=""{itemClass}""><a class=""page-link"" {anchorAttribute}>{pageNumber}</a></li>";
            return content;
        }
    }
}

