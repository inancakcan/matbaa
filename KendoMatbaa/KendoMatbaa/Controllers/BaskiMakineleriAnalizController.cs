using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class BaskiMakineleriAnalizController : BaseController
    {
        // GET: BaskiMakineleriAnaliz
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IcBaskiMakineAnalizOku()
        {
            using (var matbaa = new matbaaEntities())
            {
                ObjectResult<spIcBaskiMakineleriAnaliz_Result> icBaskiMakineleriAnaliz = matbaa.spIcBaskiMakineleriAnaliz(DateTime.Parse("2014.01.01"), DateTime.Parse("2016.01.01"));
                return Json(icBaskiMakineleriAnaliz.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult KapakBaskiMakineAnalizOku()
        {
            using (var matbaa = new matbaaEntities())
            {
                ObjectResult<spKapakBaskiMakineleriAnaliz_Result> kapakBaskiMakineleriAnaliz = matbaa.spKapakBaskiMakineleriAnaliz(DateTime.Parse("2014.01.01"), DateTime.Parse("2016.01.01"));
                return Json(kapakBaskiMakineleriAnaliz.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}