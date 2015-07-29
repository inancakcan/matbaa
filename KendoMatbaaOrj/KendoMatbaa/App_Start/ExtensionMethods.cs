using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoMatbaa.App_Start
{
    public static class ExtensionMethods
    {
        public static ActionResult SetStatusMessage(this ActionResult ar, string str)
        {
            var c = new HttpCookie("NotifyBar");
            //c.Expires = DateTime.Now.AddDays(-1);
            c.Value = str;
            HttpContext.Current.Response.Cookies.Add(c);
            return ar;
        }
    }
}