using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace KendoMatbaa.Controllers
{
    public class DeneController : BaseController
    {
        // GET: Dene
        public ActionResult Index()
        {
            return View();
        }

        KendoMatbaa.Models.matbaaEntities db = new KendoMatbaa.Models.matbaaEntities();

        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            //var model = db.IsEmri;
            //return PartialView("_GridView1Partial", model.ToList());
            return PartialView("_GridView1Partial",DataBindingToLargeDatabasePartial());
        }

        public ActionResult DataBindingToLargeDatabasePartial()
        {
            //var model = db.IsEmri;
            //return PartialView("_GridView1Partial", model.ToList());
            return PartialView("DataBindingToLargeDatabasePartial");
        }
    }
}