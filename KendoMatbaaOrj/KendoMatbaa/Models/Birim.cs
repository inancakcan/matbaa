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
    
    public partial class Birim
    {
        public Birim()
        {
            this.IsEmri = new HashSet<IsEmri>();
        }
    
        public int BirimId { get; set; }
        public string BirimAdi { get; set; }
        public string BirimKodu { get; set; }
    
        public virtual ICollection<IsEmri> IsEmri { get; set; }
    }
}
