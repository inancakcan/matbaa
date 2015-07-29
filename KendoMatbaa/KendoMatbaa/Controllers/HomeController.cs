using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace KendoMatbaa.Controllers
{
    public class HomeController : BaseController
        //public class HomeController : Controller
    {

        public ActionResult IsEmirleri()
        {
            // Add a report to the view data. 
            //ViewData["Report"] = new KendoMatbaa.Reports.IsEmriRaporu();

            return View();
        }

        public ActionResult Index()
        {
            // Add a report to the view data. 
            //ViewData["Report"] = new KendoMatbaa.Reports.IsEmriRaporu();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {ViewBag.Message = "Your contact page.";

            return View();}





        KendoMatbaa.Models.matbaaEntities db = new KendoMatbaa.Models.matbaaEntities();

        [ValidateInput(false)]
        public ActionResult IsEmriGridPartial()
        {
            var model = db.IsEmri;
            return PartialView("_IsEmriGridPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IsEmriGridPartialAddNew(KendoMatbaa.Models.IsEmri item)
        {
            var model = db.IsEmri;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_IsEmriGridPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IsEmriGridPartialUpdate(KendoMatbaa.Models.IsEmri item)
        {
            var model = db.IsEmri;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.IsEmriId == item.IsEmriId);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_IsEmriGridPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult IsEmriGridPartialDelete(System.Int32 IsEmriId)
        {
            var model = db.IsEmri;
            if (IsEmriId >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.IsEmriId == IsEmriId);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_IsEmriGridPartial", model.ToList());
        }

        

        public ActionResult IsEmriDetayDuzenleme()
        {
            //var model = db.IsEmri.FirstOrDefault(it => it.IsEmriId == IsEmriId);
            return View();
        }

    }
}