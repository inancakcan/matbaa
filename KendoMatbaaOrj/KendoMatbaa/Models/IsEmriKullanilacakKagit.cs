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
    
    public partial class IsEmriKullanilacakKagit
    {
        public int IsEmriID { get; set; }
        public Nullable<int> IcKagitCins { get; set; }
        public Nullable<int> IcKagitNet { get; set; }
        public Nullable<int> IcKagitFire { get; set; }
        public Nullable<int> IcBaskiBoyutu { get; set; }
        public Nullable<int> IcTabaka { get; set; }
        public Nullable<int> KapakCins { get; set; }
        public Nullable<int> KapakBaskiBoyutu { get; set; }
        public Nullable<int> KapakNet { get; set; }
        public Nullable<int> KapakTabaka { get; set; }
        public Nullable<int> KapakFire { get; set; }
        public Nullable<bool> Otokopi { get; set; }
        public int IsEmriKullanilacakKagitId { get; set; }
        public Nullable<bool> Silindi { get; set; }
    
        public virtual TabakaKesimleri TabakaKesimleri { get; set; }
        public virtual TabakaKesimleri TabakaKesimleri1 { get; set; }
        public virtual IsEmri IsEmri { get; set; }
        public virtual BaskiEbatlari BaskiEbatlari { get; set; }
        public virtual BaskiEbatlari BaskiEbatlari1 { get; set; }
        public virtual AltGruplar AltGruplar { get; set; }
        public virtual AltGruplar AltGruplar1 { get; set; }
    }
}
