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
    
    public partial class StokHareketSilmeTarihce
    {
        public long StokHareketSilmeTarihceId { get; set; }
        public Nullable<int> IsEmriId { get; set; }
        public Nullable<double> Miktar { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Hareket { get; set; }
        public string Personel { get; set; }
        public string IsEmri { get; set; }
        public string Yon { get; set; }
        public string KimNeZaman { get; set; }
    
        public virtual IsEmri IsEmri1 { get; set; }
    }
}
