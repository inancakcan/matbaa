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
    
    public partial class Uretim
    {
        public int UretimId { get; set; }
        public int IsEmriId { get; set; }
        public int AltUretimAsamalariId { get; set; }
        public System.DateTime Tarih { get; set; }
        public System.Guid Personel { get; set; }
        public Nullable<System.DateTime> UretimeBaslama { get; set; }
        public Nullable<System.DateTime> UretimiSonlandirma { get; set; }
    
        public virtual AltUretimAsamalari AltUretimAsamalari { get; set; }
        public virtual IsEmri IsEmri { get; set; }
        public virtual Personel Personel1 { get; set; }
    }
}
