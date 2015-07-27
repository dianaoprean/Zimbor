using System.Collections;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System;

namespace Infrastructure.UI.Helpers
{
    public static class Utils
    {
        public static SelectList SelectList(IEnumerable items, string valueField, string textField, string nullText)
        {
            var list = new SelectList(items, valueField, textField).ToList();
            if (nullText!=null) list.Insert(0, new SelectListItem { Text = nullText, Value = 0.ToString() });
            return new SelectList(list, "Value", "Text");
        }

        public static SelectList SelectList(IEnumerable items, string valueField, string textField, string nullText, bool isDefaultNull)
        {
            var list = new SelectList(items, valueField, textField).ToList();
            if (nullText != null) list.Insert(0, new SelectListItem { Text = nullText, Value = isDefaultNull ? string.Empty : 0.ToString() });
            return new SelectList(list, "Value", "Text");
        }

        public static SelectList SelectList(IEnumerable items, string valueField, string textField, string nullText, string selectedValue)
        {
            var list = new SelectList(items, valueField, textField, selectedValue).ToList();
            var selected = list.Where(x => x.Value == selectedValue).FirstOrDefault();
            if (selected != null) selected.Selected = true;

            if (nullText != null) list.Insert(0, new SelectListItem { Text = nullText, Value = 0.ToString() });
            return new SelectList(list, "Value", "Text", selectedValue);
        }

        public static SelectList SelectListTextValue(IEnumerable items, string valueField, string textField, string nullText)
        {
            var list = new SelectList(items, valueField, textField).ToList();
            if (nullText != null) list.Insert(0, new SelectListItem { Text = nullText, Value = string.Empty });
            return new SelectList(list, "Value", "Text");
        }

        public static SelectList SelectListTextValue(IEnumerable items, string valueField, string textField, string nullText, string nullTextValue)
        {
            var list = new SelectList(items, valueField, textField).ToList();
            if (nullText != null) list.Insert(0, new SelectListItem { Text = nullText, Value = nullTextValue });
            return new SelectList(list, "Value", "Text");
        }

        public static SelectList YesNoSelectList()
        {
            return new SelectList(new List<SelectListItem>()
                {
                    new SelectListItem(){ Text = "Yes", Value = "True" },
                    new SelectListItem(){ Text = "No", Value = "False" }
                }, "Value", "Text");
        }

        public static string CleanText(string text)
        {
            return Regex.Replace(text, @"[\W]", "").Trim();
        }

        public static string RenderPartialViewtoString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
