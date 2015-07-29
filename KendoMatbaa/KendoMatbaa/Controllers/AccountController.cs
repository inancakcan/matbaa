using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KendoMatbaa.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnUrl ?? Url.Action("Index", "IsEmri"));
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            //HttpContext currentContext = System.Web.HttpContext.Current;
            //string userName = currentContext.User.Identity.Name;
            return RedirectToAction("Index", "Home");
        }
	}
}