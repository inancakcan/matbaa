using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.DashboardWeb.Mvc;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Web.Mvc;

namespace KendoMatbaa.Controllers
{
    public class KagitTuketimiDashBoardController : BaseController
    {
        // GET: KagitTuketimiDashBoard
        public ActionResult Index()
        {
            return View();
        }



        [ValidateInput(false)]
        public ActionResult KagitTuketimiDashboardViewerPartial()
        {
            return PartialView("_KagitTuketimiDashboardViewerPartial", KagitTuketimiDashboardViewerSettings.Model);
        }
        public FileStreamResult KagitTuketimiDashboardViewerPartialExport()
        {
            return DevExpress.DashboardWeb.Mvc.DashboardViewerExtension.Export("KagitTuketimiDashboardViewer", KagitTuketimiDashboardViewerSettings.Model);
        }

        KendoMatbaa.Models.matbaaEntities db = new KendoMatbaa.Models.matbaaEntities();

        [ValidateInput(false)]
        public ActionResult KagitTuekitimiPivotGridPartial()
        {
            var model = db.vKagitTuketimi;
            return PartialView("_KagitTuekitimiPivotGridPartial", model.ToList());
        }

        public ActionResult KagitTuekitimiPivot()
        {
            return View();
        }
    
    }
    class KagitTuketimiDashboardViewerSettings
    {
        public static DevExpress.DashboardWeb.Mvc.DashboardSourceModel Model
        {
            //get
            //{
            //    return new DevExpress.DashboardWeb.Mvc.DashboardSourceModel();
            //}



            get
            {
                DashboardSourceModel model = new DashboardSourceModel();
                model.DashboardId = "matbaa";
                model.DashboardLoading = (sender, e) =>
                {
                    // Checks the identifier of the required dashboard.
                    if (e.DashboardId == "matbaa")
                    {
                        string dashboardDefinition = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~\DashBoards\matbaa.xml"));
                        e.DashboardXml = dashboardDefinition;
                    }
                };

                model.ConfigureDataConnection = (sender, e) =>
                {
                    if (e.ConnectionName == "localhost_Connection")
                    {
                        MsSqlConnectionParameters parameters = (MsSqlConnectionParameters)e.ConnectionParameters;
                        //parameters.DatabaseName = "sfKalibrasyon";
                        parameters.UserName = "sa";
                        parameters.Password = "!a1478963a!";
                    }
                };

                return model;
            }}
    }

}