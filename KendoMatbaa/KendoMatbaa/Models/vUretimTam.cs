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
    
    public partial class vUretimTam
    {
        public int UretimId { get; set; }
        public int IsEmriId { get; set; }
        public System.DateTime Tarih { get; set; }
        public string AltUretimAsamalariAdi { get; set; }
        public Nullable<int> UretimNotlariId { get; set; }
        public string TasarimaOnayVeren { get; set; }
        public string BaskiOncesiHazirlikNotu { get; set; }
        public string BaskiNotu { get; set; }
        public string MucellithaneNotu { get; set; }
        public string IsKesimiProgramNo { get; set; }
        public string PaketBilgileri { get; set; }
        public string PersonelAdiSoyadi { get; set; }
        public int SicilNo { get; set; }
        public bool Admin { get; set; }
        public string UretimAsamasiAdi { get; set; }
        public Nullable<int> TeslimatRaporuId { get; set; }
        public Nullable<System.Guid> TeslimEdenPersonel { get; set; }
        public string TeslimAlan { get; set; }
        public Nullable<int> TeslimatMiktari { get; set; }
        public System.Guid PersonelUN { get; set; }
        public int AltUretimAsamalariId { get; set; }
        public int UretimAsamalariId { get; set; }
    }
}