﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace YuNotes.TagHelpers
{
    public class SortNotesTagHelper : TagHelper
    {
        public SortState Property { get; set; }
        public Guid? GroupId { get; set; }

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
            string? url = urlHelper.Action(Action, new { sortOrder = Property, groupId = TryGetGroupId() });
            output.Attributes.SetAttribute("href", url);    
        }

        private Guid? TryGetGroupId()
        {
            if (GroupId == Guid.Empty)
                GroupId = null;
            return GroupId;
        }
    }
}