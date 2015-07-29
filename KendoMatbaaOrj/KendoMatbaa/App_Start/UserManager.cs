using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using KendoMatbaa.Models;
using KendoMatbaa.YetkiServis;
using System.Data;

namespace KendoMatbaa.App_Start
{
    public static class UserManager
    {
        /// <summary>
        /// Returns the User from the Context.User.Identity by decrypting the forms auth ticket and returning the user object.
        /// </summary>
        public static User User
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // The user is authenticated. Return the user from the forms auth ticket.
                    return ((MyPrincipal)(HttpContext.Current.User)).User;
                }
                else if (HttpContext.Current.Items.Contains("User"))
                {
                    // The user is not authenticated, but has successfully logged in.
                    return (User)HttpContext.Current.Items["User"];
                }
                else
                {
                    return null;
                }
            }
        }
        /*
        /// <summary>
        /// Authenticates a user against a database, web service, etc.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User</returns>
        public static User AuthenticateUser(string username, string password)
        {
            User user = null;

            // Lookup user in database, web service, etc. We'll just generate a fake user for this demo.
            if (username == "john" && password == "doe")
            {
                user = new User { Id = 123, Name = "John Doe", Username = "johndoe", Age = 21 };
            }

            return user;
        }
         */


        /// <summary>
        /// Authenticates a user against a database, web service, etc.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User</returns>
        public static User AuthenticateUser(string username, string password)
        {
            User user = null;
            using (YetkiServisSoapClient client = new YetkiServisSoapClient())
            {
                KullaniciDataSet dsKullaniciDataSet = client.KullaniciGiris(username, password);
                if (dsKullaniciDataSet.Tables[0].Rows.Count > 0)
                {
                    user = new User
                    {
                        KullaniciUN = Guid.Parse(dsKullaniciDataSet.Tables[0].Rows[0]["KullaniciUN"].ToString()),
                        Name = dsKullaniciDataSet.Tables[0].Rows[0]["Adi"].ToString() + " " + dsKullaniciDataSet.Tables[0].Rows[0]["Soyadi"].ToString(),
                        Username = dsKullaniciDataSet.Tables[0].Rows[0]["KullaniciAdi"].ToString(),
                        Age = 21,
                        EPosta = dsKullaniciDataSet.Tables[0].Rows[0]["EPosta"].ToString()
                    };
                    //KullaniciOturumDataSet dsKullaniciOturumDataSet= client.KullaniciOturumGetir(user.KullaniciUN, Guid.Parse("20cca136-e571-4cbb-8ec3-7ea233df2b49"), 0,ref string HataVar);
                }
            }
            return user;
        }


        /*
        /// <summary>
        /// Authenticates a user via the MembershipProvider and creates the associated forms authentication ticket.
        /// </summary>
        /// <param name="logon">Logon</param>
        /// <param name="response">HttpResponseBase</param>
        /// <returns>bool</returns>
        public static bool ValidateUser(Logon logon, HttpResponseBase response)
        {
            bool result = false;
            if (Membership.ValidateUser(logon.Username, logon.Password))
            {
                // Create the authentication ticket with custom user data.
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(UserManager.User);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        logon.Username,
                        DateTime.Now,
                        DateTime.Now.AddDays(30),
                        true,
                        userData,
                        FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                result = true;
            }

            return result;
        }
        */
        /// <summary>
        /// Authenticates a user via the MembershipProvider and creates the associated forms authentication ticket.
        /// </summary>
        /// <param name="logon">Logon</param>
        /// <param name="response">HttpResponseBase</param>
        /// <returns>bool</returns>
        public static bool ValidateUser(Logon logon, HttpResponseBase response)
        {
            bool result = false;
            if (Membership.ValidateUser(logon.Username, logon.Password))
            {
                // Create the authentication ticket with custom user data.
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(UserManager.User);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        logon.Username,
                        DateTime.Now,
                        DateTime.Now.AddDays(30),
                        true,
                        userData,
                        FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                result = true;
            }

            return result;
        }
        /// <summary>
        /// Clears the user session, clears the forms auth ticket, expires the forms auth cookie.
        /// </summary>
        /// <param name="session">HttpSessionStateBase</param>
        /// <param name="response">HttpResponseBase</param>
        public static void Logoff(HttpSessionStateBase session, HttpResponseBase response)
        {
            // Delete the user details from cache.
            session.Abandon();

            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            // Clear authentication cookie.
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(cookie);
        }

        //public static  string[]  KullanicininRolleriniDon()
        //{
        //    using (YetkiServisSoapClient client = new YetkiServisSoapClient())
        //    {
        //        string[] Roller = {string.Empty};
        //        KullaniciRolDataSet ds=  client.KullaniciRolleriGetir(User.KullaniciUN);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            int i = 0;
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Roller[i] = row["ss"].ToString();
        //            }
        //            return Roller;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}