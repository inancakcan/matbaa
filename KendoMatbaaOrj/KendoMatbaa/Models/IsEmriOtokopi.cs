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
    
    public partial class IsEmriOtokopi
    {
        public int IsEmriId { get; set; }
        public int IsEmriOtokopiId { get; set; }
        public Nullable<int> NushaNo { get; set; }
        public Nullable<int> AltGruplarId { get; set; }
        public Nullable<int> FireMiktari { get; set; }
        public Nullable<bool> Silindi { get; set; }
    
        public virtual IsEmri IsEmri { get; set; }
        public virtual AltGruplar AltGruplar { get; set; }
    }
}