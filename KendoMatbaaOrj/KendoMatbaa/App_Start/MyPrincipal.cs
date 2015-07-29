using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace KendoMatbaa.App_Start
{
    public class MyPrincipal : IPrincipal
    //public class MyPrincipal : GenericPrincipal
    {
        public MyPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public User User { get; set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}