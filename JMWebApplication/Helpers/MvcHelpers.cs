using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JMApplication.Helpers
{
    public static class MvcHelpers
    {
        public static string RenderPartialView(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                return null;

            //if (string.IsNullOrEmpty(viewName))            
            //    viewName = controller.ControllerContext.RouteData.GetRequiredString("action"); 

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw); return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Return Form Collection Value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="field"></param>
        /// <param name="index"></param>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        public static string ReturnFormCollectionValue(string array, string field, int index, FormCollection formCollection)
        {
            String key = array + "[" + index + "][" + field + "]";
            return formCollection[key].ToString();
        }

    }
}