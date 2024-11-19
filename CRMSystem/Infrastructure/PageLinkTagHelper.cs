using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using CRMSystem.ViewModels;



namespace CRMSystem.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "paging-info")]
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _factory;

        public PageLinkTagHelper(IUrlHelperFactory factory)
        {
            _factory = factory;
        }


        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PagingInfo { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public string PageAction { get; set; }
        public bool PageClassEnabled { get; set; }
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var helper = _factory.GetUrlHelper(ViewContext);

            var result = new TagBuilder("div");

            for (int i = 1; i <= PagingInfo.TotalPage; i++)
            {
                var tag = new TagBuilder("a");

                PageUrlValues["page"] = i;

                tag.Attributes["href"] = helper.Action(new UrlActionContext { Action = PageAction, Values = PageUrlValues });

                if (PageClassEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(PagingInfo.CurrentPage == i ? PageClassSelected : PageClassNormal);
                }

                tag.InnerHtml.Append(i.ToString());

                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }

}