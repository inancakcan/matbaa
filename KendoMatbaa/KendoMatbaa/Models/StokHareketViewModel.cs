using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoMatbaa.Models
{
    using System;
    using System.Collections.Generic;
    public class StokHareketViewModel
    {
        public int StokHareketId { get; set; }
        public int AltGruplarId { get; set; }public double Miktar { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Hareket { get; set; }
        public string Personel { get; set; }
        public string Yon { get; set; }public Nullable<bool> Silindi { get; set; }
        public Nullable<int> IsEmriId { get; set; }

        public virtual AltGruplar AltGruplar { get; set; }
    }
}
