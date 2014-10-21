using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;

namespace DebtGroup.Extensions
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

        /// <summary>
        /// Creates a dropdown list for an Enum property
        /// </summary>
        /// <exception cref="System.ArgumentException">If the property type is not a valid Enum</exception>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression, string emptyItemText = "", string emptyItemValue = "", object htmlAttributes = null)
        {
            return html.DropDownListFor(expression, GetEnumSelectList<TEnum>(true, emptyItemText, emptyItemValue), htmlAttributes);
        }

        /// <summary>
        /// Creates a checkbox list for an Enum property
        /// </summary>
        public static MvcHtmlString EnumCheckBoxListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TEnum>>> expression, object htmlAttributes = null)
        {
            return html.CheckBoxListFor(expression, GetEnumSelectList<TEnum>(), htmlAttributes);
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.CheckBoxList(listName, items, htmlAttributes);
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string listName, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var container = new TagBuilder("div");
            foreach (var item in items)
            {
                var label = new TagBuilder("label");
                label.MergeAttribute("class", "checkbox"); // default class
                label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);
                if (item.Selected)
                    cb.MergeAttribute("checked", "checked");

                label.InnerHtml = cb.ToString(TagRenderMode.SelfClosing) + item.Text;

                container.InnerHtml += label.ToString();
            }

            return new MvcHtmlString(container.ToString());
        }

        /// <summary>
        /// A helper for wiring up Twitter bootstrap's Typeahead component.
        /// </summary>
        /// <param name="items">The list to be serialized as JSON and assigned to the data-items attribute of the textbox</param>
        public static MvcHtmlString TypeaheadFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<string> source, int items = 8)
        {
            if (expression == null || source == null)
            {
                throw new ArgumentNullException();                
            }            
            var jsonString = new JavaScriptSerializer().Serialize(source);

            return htmlHelper.TextBoxFor(expression, new { autocomplete = "off", data_provide = "typeahead", data_items = items, data_source = jsonString });
        }

        private static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>(bool addEmptyItemForNullable = false, string emptyItemText = "", string emptyItemValue = "")
        {
            var enumType = typeof(TEnum);
            var nullable = false;

            if (!enumType.IsEnum)
            {
                enumType = Nullable.GetUnderlyingType(enumType);

                if (enumType != null && enumType.IsEnum)
                {
                    nullable = true;
                }
                else
                {
                    throw new ArgumentException("Not a valid Enum type");
                }
            }

            var selectListItems = (from v in Enum.GetValues(enumType).Cast<TEnum>()
                                   select new SelectListItem
                                   {
                                       Text = v.ToString(),
                                       Value = v.ToString()
                                   }).ToList();

            if (nullable && addEmptyItemForNullable)
            {
                selectListItems.Insert(0, new SelectListItem { Text = emptyItemText, Value = emptyItemValue });
            }

            return selectListItems;
        }

        private static IEnumerable<SelectListItem> GetCheckboxListWithDefaultValues(object defaultValues, IEnumerable<SelectListItem> selectList)
        {
            var defaultValuesList = defaultValues as IEnumerable;

            if (defaultValuesList == null)
                return selectList;

            IEnumerable<string> values = from object value in defaultValuesList
                                         select Convert.ToString(value, CultureInfo.CurrentCulture);

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            selectList.ForEach(item =>
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            });

            return newSelectList;
        }
	}
}

