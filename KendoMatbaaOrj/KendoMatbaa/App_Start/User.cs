using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoMatbaa.App_Start
{
    public class User
    {
        public Guid KullaniciUN { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }

        public string EPosta { get; set; }      

        public string[] Roller { get; set; }
    }
}