using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;

namespace KendoMatbaa.Controllers
{
    public class UretimFormuController : BaseController
    {
        //
        // GET: /UretimFormu/
        public ActionResult UretimFormu(int IsEmriId)
        {
            using (matbaaEntities ent =new matbaaEntities())
            {
                if (ent.spAltUretimAsamasiAltUretimAsamasiIdden(IsEmriId, 1).Count() > 0)
                {
                    var result = ent.spAltUretimAsamasiAltUretimAsamasiIdden(IsEmriId, 1).FirstOrDefault();
                    return View(result);
                }
                else
                {
                    return RedirectToAction("Create", "Uretim").Warning("Bu İş Emri için ilgili üretim bilgisi henüz girilmemiş!");
                }
            }
            
        }

        //public ActionResult IsEmriYetkilendirme(int IsEmriId)
        //{

        //}

        public ActionResult HerhangiBirUretimNotunuDon(int AsamaId,int IsEmriId)
        {
            string Not = string.Empty;
            using (matbaaEntities db = new matbaaEntities())
            {
                if (AsamaId == 1)
                {
                    Not = (from c in db.UretimNotlari
                        where c.IsEmriId == IsEmriId
                        select new {c.BaskiOncesiHazirlikNotu}).First().BaskiOncesiHazirlikNotu;
                }

                if (AsamaId == 2)
                {
                    Not = (from c in db.UretimNotlari
                           where c.IsEmriId == IsEmriId
                           select new { c.BaskiNotu }).First().BaskiNotu;
                }

                if (AsamaId == 3)
                {
                    Not = (from c in db.UretimNotlari
                           where c.IsEmriId == IsEmriId
                           select new { c.MucellithaneNotu }).First().MucellithaneNotu;
                }
            }
            return View(Not);
        }
	}
}