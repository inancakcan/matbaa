using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoMatbaa.Models
{
    public class BarChartsLocalDataViewModel : KritikSeviye
    {
        public BarChartsLocalDataViewModel()
        {
        }


        public BarChartsLocalDataViewModel(int altGruplarId, int kritikStok, int guncelStok, string altGrupAdi)
        {
            AltGruplarId = altGruplarId;
            KritikStok = kritikStok;
            GuncelStok = guncelStok;
            AltGrupAdi = altGrupAdi;
        }


        //public int KritikSeviyeId { get; set; }
        //public int KritikStok { get; set; }
        //public int GüncelStok { get; set; }
        //public int AltGruplarId { get; set; }

        //public virtual AltGruplar AltGruplar { get; set; }

        public string UserColor { get; set; }
        public string AltGrupAdi { get; set; }
    }
}