using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using KendoMatbaa.Reports;
using DevExpress.Web.Mvc;

namespace KendoMatbaa.Controllers
{
    public class ReportController : BaseController
    {
        private static int mIsEmriId;
        public static int isEmriId
        {
            get { return mIsEmriId; }
            set { mIsEmriId = value; }
        }
        // GET: Report
        public ActionResult Index(int IsEmriId)
        {
            isEmriId = IsEmriId;
            // Add a report to the view data. 
            //ViewData["Report"] = new KendoMatbaa.Reports.IsEmriRaporu();
            //subIsEmriLaminasyonlari rIsEmriLaminasyonlari = new subIsEmriLaminasyonlari();
            //rIsEmriLaminasyonlari.Parameters["subLaminasyonIsEmriId"].Value = 100478777;
            //rIsEmriLaminasyonlari.Parameters["subLaminasyonIsEmriId"].Visible = false;
            //ViewData["Report"] = CreateReport();
            return View();
        }


        public ActionResult preTeslimatRaporu()
        {
            return View();
        }


        //public ActionResult TeslimatRaporu(DateTime BasTar, DateTime SonTar)
        //{
        //    return View();
        //}
       

        public static Reports.IsEmriRaporu CreateReport()
        {
            Reports.IsEmriRaporu report = new Reports.IsEmriRaporu();
            report.DataSourceDemanded += new System.EventHandler<System.EventArgs>(report_DataSourceDemanded);

            return report;
        }

        private static void report_DataSourceDemanded(object sender, System.EventArgs e)
        {
            //int bak = isEmriId;
            Reports.IsEmriRaporu report = (Reports.IsEmriRaporu)sender;
            using (matbaaEntities ent = new matbaaEntities())
            {

                //parametre olark IsEmriId Sadece burada gönderilmesi yeterli
                report.pIsEmriId.Value = isEmriId;//100478777;//isEmriId;
                report.pIsEmriId.Visible = false;

                // report.DataSource = ent.spIsEmriRaporu(100478777).ToList();
            }
        }
        public ActionResult DocumentViewerPartial()
        {
            //isEmriId = IsEmriId; 
            ViewData["Report"] = CreateReport();
            return PartialView("DocumentViewerPartial");
        }

        public ActionResult ExportDocumentViewer()
        {
            //return DevExpress.Web.Mvc.DocumentViewerExtension.ExportTo(new KendoMatbaa.Reports.XtraReport1());
            return DevExpress.Web.Mvc.DocumentViewerExtension.ExportTo(CreateReport());
        }

        #region TeslimatRaporu
        KendoMatbaa.Reports.Teslimat_Raporu report = new KendoMatbaa.Reports.Teslimat_Raporu();

        public ActionResult TeslimatRaporu()
        {
            return View();
        }

        public ActionResult TeslimatRaporuPartial()
        {
            return PartialView("_TeslimatRaporuPartial", report);
        }

        public ActionResult TeslimatRaporuPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
        #endregion TeslimatRaporu
        
        #region IcKagitTuketimiRaporu
        KendoMatbaa.Reports.IcKagitTuketimi reportIcKagitTuketimi = new KendoMatbaa.Reports.IcKagitTuketimi();
        public ActionResult IcKagitTuketimiRaporu()
        {
            return View();
        }
        public ActionResult IcKagitTuketimiRaporuPartial()
        {
            return PartialView("_IcKagitTuketimiRaporuPartial", reportIcKagitTuketimi);
        }

        public ActionResult IcKagitTuketimiRaporuPartialExport()
        {
            return DocumentViewerExtension.ExportTo(reportIcKagitTuketimi, Request);
        }
        #endregion TeslimatRaporu

        public ActionResult TeslimatRaporunuOlustur(DateTime basTar, DateTime sonTar)
        {
            return RedirectToAction("TeslimatRaporu", "Report", new { BasTar = basTar, SonTar = sonTar });
        }
    }
}