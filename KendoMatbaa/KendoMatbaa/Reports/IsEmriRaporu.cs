using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace KendoMatbaa.Reports
{
    public partial class IsEmriRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public IsEmriRaporu()
        {
            InitializeComponent();
        }

        private void IsEmriRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            //IsEmriRaporu s = new IsEmriRaporu();
            //s.Parameters["pIsEmriId"].Value = 100478778;
            //s.Parameters["pIsEmriId"].Visible = false;
            
        }
        private void xrsubIsEmriLaminasyonlari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((subIsEmriLaminasyonlari)((XRSubreport)sender).ReportSource).subLaminasyonIsEmriId.Value =
      Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));
        }

        private void xrSubreportBaskiMakineleriIc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((subBaskiBilgileriIc)((XRSubreport)sender).ReportSource).BaskiMakineleriIcIsEmriId.Value =
     Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));
        }

        private void xrSubreportBaskiMakineleriKapak_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((subBaskiBilgileriKapak)((XRSubreport)sender).ReportSource).BaskiBilgileriKapakIsEmriId.Value =
  Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));
        }

        private void xrSubFilmMaliyeti_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((subFilmMaliyeti)((XRSubreport)sender).ReportSource).FilmMaliyetiIsEmriId.Value =
 Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));}

        private void xrSubKagitMaliyeti_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((subKagitMaliyeti)((XRSubreport)sender).ReportSource).KagitMaliyetiIsEmriId.Value =
Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));
        }

        private void xrSubKalipMaliyeti_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((subKalipMaliyeti)((XRSubreport)sender).ReportSource).KalipMaliyetiIsEmriId.Value =
Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));
        }
    }

    }

