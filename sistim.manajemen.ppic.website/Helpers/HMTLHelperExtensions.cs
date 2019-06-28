using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Helpers
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";
            
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            
            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController ?
                cssClass : String.Empty;
        }
        public static string IsSelectedMenu(this HtmlHelper html, string Menu = null, string CurrentMenu = "Home" , string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

           
            if (String.IsNullOrEmpty(Menu))
                Menu = CurrentMenu;
            

            return Menu == CurrentMenu ?
                cssClass : String.Empty;
        }
        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}