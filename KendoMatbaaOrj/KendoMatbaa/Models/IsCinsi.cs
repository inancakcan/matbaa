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
    
    public partial class IsCinsi
    {
        public IsCinsi()
        {
            this.IsEmriTeknikBilgiler = new HashSet<IsEmriTeknikBilgiler>();
        }
    
        public int IsCinsiId { get; set; }
        public string CinsAdi { get; set; }
        public bool Silindi { get; set; }
    
        public virtual ICollection<IsEmriTeknikBilgiler> IsEmriTeknikBilgiler { get; set; }
    }
}
