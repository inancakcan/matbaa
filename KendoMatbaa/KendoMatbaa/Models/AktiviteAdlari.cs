//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KendoMatbaa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AktiviteAdlari
    {
        public AktiviteAdlari()
        {
            this.AktiviteSahipleri = new HashSet<AktiviteSahipleri>();
        }
    
        public int AktiviteAdlariId { get; set; }
        public string AktiviteAdi { get; set; }
    
        public virtual ICollection<AktiviteSahipleri> AktiviteSahipleri { get; set; }
    }
}