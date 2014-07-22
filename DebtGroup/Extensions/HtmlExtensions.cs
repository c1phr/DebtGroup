using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace DebtGroup.HtmlExtensions
{
	public static class HtmlExtensions
	{


		public static string ActionButton(this HtmlHelper helper, string value, 
			string action, string controller, object routeValues)
		{
			var a = (new UrlHelper(helper.ViewContext.RequestContext))
				.Action(action, controller, routeValues);

			var form = new TagBuilder("form");
			form.Attributes.Add("method", "get");
			form.Attributes.Add("action", a);

			var input = new TagBuilder("input");
			input.Attributes.Add("type", "submit");
			input.Attributes.Add("value", value);
			input.Attributes.Add ("class", "btn btn-primary");

			form.InnerHtml = input.ToString(TagRenderMode.SelfClosing);

			return form.ToString(TagRenderMode.Normal);
		}

		public static string ActionButton(this HtmlHelper helper, string value, 
			string action, string controller, object routeValues, string additionalClasses)
		{
			var a = (new UrlHelper(helper.ViewContext.RequestContext))
				.Action(action, controller, routeValues);

			var form = new TagBuilder("form");
			form.Attributes.Add("method", "get");
			form.Attributes.Add("action", a);

			var input = new TagBuilder("input");
			input.Attributes.Add("type", "submit");
			input.Attributes.Add("value", value);
			input.Attributes.Add ("class", "btn btn-primary " + additionalClasses);

			form.InnerHtml = input.ToString(TagRenderMode.SelfClosing);

			return form.ToString(TagRenderMode.Normal);
		}
	}
}

