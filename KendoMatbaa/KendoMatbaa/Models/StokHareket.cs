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
    
    public partial class StokHareket
    {
        public int StokHareketId { get; set; }
        public int AltGruplarId { get; set; }
        public double Miktar { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Hareket { get; set; }
        public string Personel { get; set; }
        public string Yon { get; set; }
        public Nullable<bool> Silindi { get; set; }
        public Nullable<int> IsEmriId { get; set; }
    
        public virtual AltGruplar AltGruplar { get; set; }
    }
}