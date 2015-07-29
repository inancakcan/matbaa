using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoMatbaa.Models
{
    public class MenuModel
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int ParentId { get; set; }
        //public int SortOrder { get; set; }

        public Guid MenuUN { get; set; }
        public string MenuAdi { get; set; }
        public string MenuUrl { get; set; }
        public Guid ParentUN { get; set; }
        public int Sira { get; set; }
    }
}