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
    
    public partial class TabakaKesimleri
    {
        public TabakaKesimleri()
        {
            this.IsEmriKullanilacakKagit = new HashSet<IsEmriKullanilacakKagit>();
            this.IsEmriKullanilacakKagit1 = new HashSet<IsEmriKullanilacakKagit>();
        }
    
        public int TabakaKesimleriId { get; set; }
        public string TabakaKesimi { get; set; }
    
        public virtual ICollection<IsEmriKullanilacakKagit> IsEmriKullanilacakKagit { get; set; }
        public virtual ICollection<IsEmriKullanilacakKagit> IsEmriKullanilacakKagit1 { get; set; }
    }
}