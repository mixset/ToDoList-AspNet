using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Utils.Helpers
{
    public static class SliderHelper
    {
        public static MvcHtmlString Slider(this HtmlHelper helper, string container_id, List<string> images)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<div id=\"{0}\" class=\"carousel slide\" data-ride=\"carousel\" data-interval=\"1500\">", container_id);
                sb.Append("<div class=\"carousel-inner\" role=\"listbox\">");

                sb.Append("<div class=\"item active\">");
                sb.AppendFormat("<img src=\"/Content/Images/{0}\" style=\"width:100%;height:250px;\">", images.First());
                sb.Append("</div>");

                foreach (var image in images.Skip(1)) 
                {
                    sb.Append("<div class=\"item\">");
                    sb.AppendFormat("<img src=\"/Content/Images/{0}\" style=\"width:100%;height:250px;\">", image);
                    sb.Append("</div>");
                }

               /*
                sb.AppendFormat("<a class=\"left carousel-control\" href=\"{0}\" role=\"button\" data-slide=\"prev\">", container_id);
                    sb.Append("<span class=\"glyphicon glyphicon-chevron-left\" aria-hidden=\"true\" ></span>");
                    sb.Append("<span class=\"sr-only\">Previous</span>");
                sb.Append("</a>");

                sb.AppendFormat("<a class=\"right carousel-control\" href=\"{0}\" role=\"button\" data-slide=\"next\">", container_id);
                    sb.Append("<span class=\"glyphicon glyphicon-chevron-right\" aria-hidden=\"true\" ></span>");
                    sb.Append("<span class=\"sr-only\">Next</span>");
                sb.Append("</a>");
                */

                sb.Append("</div>");
            sb.Append("</div>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}
