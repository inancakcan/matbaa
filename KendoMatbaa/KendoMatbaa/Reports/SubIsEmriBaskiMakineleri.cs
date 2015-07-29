using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace KendoMatbaa.Reports
{
    public partial class SubIsEmriBaskiMakineleri : DevExpress.XtraReports.UI.XtraReport
    {
        public SubIsEmriBaskiMakineleri()
        {
            InitializeComponent();
        }

        private void SubIsEmriBaskiMakineleri_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((XRSubreport)sender).ReportSource.Parameters["parameter1"].Value as SubIsEmriBaskiMakineleri = Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));
            //   (SubIsEmriBaskiMakineleri)((XRSubreport)sender).ReportSource.Parameters["parameter1"].Value =
            //Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));

            //     ((XtraReport)((XRSubreport)sender).ReportSource).Parameters["parameter1"].Value =
            //Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));


            //SubIsEmriBaskiMakineleri s = new SubIsEmriBaskiMakineleri();
            //s.Parameters["parameter1"].Value = 100478777;
            //s.Parameters["parameter1"].Visible = false;}


            ////    ((SubIsEmriBaskiMakineleri)((XRSubreport)sender).ReportSource).parameter2.Value =
            ////        Convert.ToInt32(GetCurrentColumnValue("IsEmriId"));


        }
        }
}
