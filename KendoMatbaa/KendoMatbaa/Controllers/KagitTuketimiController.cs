using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class KagitTuketimiController : BaseController
    {
        // GET: KagitTuketimi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Ic()
        {
            return PartialView("_Ic");
        }
        public ActionResult _Kapak(){
            return PartialView("_Kapak");
        }

        public ActionResult _IcFire()
        {
            return PartialView("_IcFire");
        }

        public ActionResult _KapakFire()
        {
            return PartialView("_KapakFire");
        }
        public ActionResult KagitTuketimiAnaliz()
        {
            return View();
        }

      public ActionResult IcKagitTuketimiAnalizOku()
        {
            using (var matbaa = new matbaaEntities())
            {
                ObjectResult<spIcKagitTuketimiAnaliz_Result> icKagitTuketimiAnaliz = matbaa.spIcKagitTuketimiAnaliz(DateTime.Parse("2014.01.01"), DateTime.Parse("2016.01.01"));
                return Json(icKagitTuketimiAnaliz.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

      public ActionResult KapakKagitTuketimiAnalizOku()
      {
          using (var matbaa = new matbaaEntities())
          {
              ObjectResult<spKapakKagitTuketimiAnaliz_Result> kapakKagitTuketimiAnaliz = matbaa.spKapakKagitTuketimiAnaliz(DateTime.Parse("2014.01.01"), DateTime.Parse("2016.01.01"));
              return Json(kapakKagitTuketimiAnaliz.ToList(), JsonRequestBehavior.AllowGet);
          }
      }

      public ActionResult IcFireKagitTuketimiAnalizOku()
      {
          using (var matbaa = new matbaaEntities())
          {
              ObjectResult<spIcFireTuketimiAnaliz_Result> icFireKagitTuketimiAnaliz = matbaa.spIcFireTuketimiAnaliz(DateTime.Parse("2014.01.01"), DateTime.Parse("2016.01.01"));
              return Json(icFireKagitTuketimiAnaliz.ToList(), JsonRequestBehavior.AllowGet);
          }
      }

      public ActionResult KapakFireKagitTuketimiAnalizOku()
      {
          using (var matbaa = new matbaaEntities())
          {
              ObjectResult<spKapakFireTuketimiAnaliz_Result> kapakFireKagitTuketimiAnaliz = matbaa.spKapakFireTuketimiAnaliz(DateTime.Parse("2014.01.01"), DateTime.Parse("2016.01.01"));
              return Json(kapakFireKagitTuketimiAnaliz.ToList(), JsonRequestBehavior.AllowGet);
          }
      }
        
        //public ActionResult _spGelenIsYogunlugu()//string ZamanAraligiBirim)
        //{
        //    using (var matbaa = new matbaaEntities())
        //    {
        //        ObjectResult<spIcKagitTuketimiAnaliz_Result> icKagitTuketimiAnaliz = matbaa.spIcKagitTuketimiAnaliz(DateTime.MinValue, DateTime.Now);
        //        return Json(icKagitTuketimiAnaliz.ToList(), JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
        KendoMatbaa.Models.matbaaEntities db = new KendoMatbaa.Models.matbaaEntities();

        public ActionResult ChartPartial()
        {
            var model = db.vKagitTuketimi;
            return PartialView("_ChartPartial", model.ToList());
        }
}
}