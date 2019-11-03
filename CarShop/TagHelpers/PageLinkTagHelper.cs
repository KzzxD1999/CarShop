using CarShop.BL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

        
            // набор ссылок будет представлять список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            if (PageModel.PageNumber > 1)
            {
                TagBuilder prevItem = CreateTag(1, urlHelper);
                tag.InnerHtml.AppendHtml(prevItem);
            }
            //for (int i = 2; i < PageModel.TotalPages; i++)
            //{

            //    if (i < 5)
            //    {
            //        TagBuilder currentItem1 = CreateTag(i, urlHelper);
            //        tag.InnerHtml.AppendHtml(currentItem1);
           
            //    }

            //}
            if(PageModel.TotalPages > 5)
            {
                TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);
                if (PageModel.PreviousPage)
                {
                    if (PageModel.PageNumber > 2)
                    {
                        TagBuilder tagBuilder = CreateTag(PageModel.PageNumber - 1, urlHelper);
                        tag.InnerHtml.AppendHtml(tagBuilder);
                    }
                 }
                
                tag.InnerHtml.AppendHtml(currentItem);
 
                if (PageModel.NextPage)
                {
                    TagBuilder tagBuilder = CreateTag(PageModel.PageNumber + 1, urlHelper);
                    tag.InnerHtml.AppendHtml(tagBuilder);
                }

                if (PageModel.PageNumber < PageModel.TotalPages && PageModel.PageNumber + 1 != PageModel.TotalPages)
                {
                    tag.InnerHtml.AppendHtml("....");
                    TagBuilder up = CreateTag(PageModel.TotalPages, urlHelper);
                    tag.InnerHtml.AppendHtml(up);
                }
                
            }


                       //создаем ссылку на следующую страницу, если она есть
            //TagBuilder nextItem = CreateTag(PageModel.PageNumber + max, urlHelper);
             //   tag.InnerHtml.AppendHtml(nextItem);
            // формируем три ссылки - на текущую, предыдущую и следующую
            //TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);

            // создаем ссылку на предыдущую страницу, если она есть



            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == this.PageModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
            }
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
