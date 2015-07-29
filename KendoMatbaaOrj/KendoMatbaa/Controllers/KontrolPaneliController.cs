using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;
using System.IO;

namespace KendoMatbaa.Controllers
{
    public class KontrolPaneliController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /KontrolPaneli/
        public ActionResult Index()
        {
            //var kritikseviye = db.KritikSeviye.Include(k => k.AltGruplar);
            //return View(kritikseviye.ToList());
            return View();}

        // GET: /KontrolPaneli/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KritikSeviye kritikseviye = db.KritikSeviye.Find(id);
            if (kritikseviye == null)
            {
                return HttpNotFound();
            }
            return View(kritikseviye);
        }

        // GET: /KontrolPaneli/Create
        public ActionResult Create()
        {
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi");
            return View();
        }

        // POST: /KontrolPaneli/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KritikSeviyeId,KritikStok,GüncelStok,AltGruplarId")] KritikSeviye kritikseviye)
        {
            if (ModelState.IsValid)
            {
                db.KritikSeviye.Add(kritikseviye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", kritikseviye.AltGruplarId);
            return View(kritikseviye);
        }

        // GET: /KontrolPaneli/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KritikSeviye kritikseviye = db.KritikSeviye.Find(id);
            if (kritikseviye == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", kritikseviye.AltGruplarId);
            return View(kritikseviye);
        }

        // POST: /KontrolPaneli/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KritikSeviyeId,KritikStok,GüncelStok,AltGruplarId")] KritikSeviye kritikseviye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kritikseviye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", kritikseviye.AltGruplarId);
            return View(kritikseviye);
        }

        // GET: /KontrolPaneli/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KritikSeviye kritikseviye = db.KritikSeviye.Find(id);
            if (kritikseviye == null)
            {
                return HttpNotFound();
            }
            return View(kritikseviye);
        }

        // POST: /KontrolPaneli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KritikSeviye kritikseviye = db.KritikSeviye.Find(id);
            db.KritikSeviye.Remove(kritikseviye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region inanc
       

        public ActionResult StokKritikSeviyeler_Read()
        {
            using (var matbaa = new matbaaEntities())
            {
                ObjectResult<spKritikSeviyeKontrol_Result> kritikSeviyes = matbaa.spKritikSeviyeKontrol();
                //return Json(kritikSeviyes);
                return Json(kritikSeviyes.ToList(),JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult _spGelenIsYogunlugu()//string ZamanAraligiBirim)
        {
            using (var matbaa = new matbaaEntities())
            {
                ObjectResult<spGelenIsYogunlugu_Result> gelenIsYogunlugu = matbaa.spGelenIsYogunlugu("m");
                //return Json(gelenIsYogunlugu.ToList(), JsonRequestBehavior.AllowGet);
                return Json(gelenIsYogunlugu.ToList(),JsonRequestBehavior.AllowGet);
            }
        }

        public ViewResult DashBoard()
        {
            return View();
        }


        public ViewResult _KritikMalzemeSeviyeleri()
        {
            return View();
        }
        public ViewResult _OnayBekleyenIsSayisi()
        {
            return View();
        }
        public ViewResult _PersonelUzerindekiIsler()
        {
            var personelUserindekiIsler = db.uspUretimYetkilendirmeArama(null, null, true);
            return View(personelUserindekiIsler.ToList());
        }

        public int OnayBekleyenIsSayisi()
        {
            int onayBekleyenIsSayisi = db.IsEmriOnayBilgileri.Where(c => c.Onayalindi == false && c.Reddedildi == false).Count();
            ViewBag.onayBekleyenIsSayisi = onayBekleyenIsSayisi;
            return onayBekleyenIsSayisi;
        }

        public ActionResult OnayBekleyenIsler()
        {
            var onayBekleyenIsler = db.spIsemriOnayBilgileriIleBirlikte();
            return View(onayBekleyenIsler.ToList());
        }

        public ActionResult _spAylaraGoreKagitHarcamasi()
        {
            //var oAylikKagitTuketimi = db.spAylaraGoreKagitHarcamasi();
            //return View(oAylikKagitTuketimi.ToList());
            return View();
        }

        public ActionResult spAylaraGoreKagitHarcamasi_Read()
        {
            using (var matbaa = new matbaaEntities())
            {
                ObjectResult<spAylaraGoreKagitHarcamasi_Result> oKagitHarcamalari = matbaa.spAylaraGoreKagitHarcamasi();
                //return Json(kritikSeviyes);
                return Json(oKagitHarcamalari.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult IsEmriniOnaylaGerc2(int id = 0)
        {
            bool Sonuc = false;
            try
            {

                using (matbaaEntities db = new matbaaEntities())
                {
                    //int IsEmriBaskiBilgileriId = FindIsEmriBaskiBilgileriByIsEmriId(id);
                    //IsEmriBaskiBilgileri tblIsEmriBaskiBilgileri = db.IsEmriBaskiBilgileri.Find(IsEmriBaskiBilgileriId);
                    //tblIsEmriBaskiBilgileri.Onayalindi = true;
                    //db.SaveChanges();
                    //Sonuc = true;
                    //Başarılı onaylama işleminden sonra stoktan malzemeleri düşmek gerekli

                    db.spIsEmrineOnayVer(id, UserManager.User.Username);
                    Sonuc = true;
                }
            }
            catch (Exception)
            {

            }
            //return Redirect("~/KontrolPaneli/Dashboard").SetStatusMessage("Onaylama başarılı") ;
            //return RedirectToAction("Dashboard").SetStatusMessage("Onaylama başarılı");
            return RedirectToAction("Dashboard").Success("Onay verme başarılı");
            //return Redirect("~/IsEmriBaskiBilgileri/Details/" + id.ToString());
            //return View("Details2", "tblIsEmriBaskiBilgileri", new {id = "#=ObjectID#"})
        }

        //public ActionResult Son30GunIcindeTeslimEdilenIsSayisi()
        //{
        //    using (matbaaEntities db = new matbaaEntities())
        //     {
        //         int Son30GunIcindeTeslimEdilenIsSayisi = db.spGecenOtuzGunIcindeBitirilenIsler().FirstOrDefault().Value;
        //            return View(Son30GunIcindeTeslimEdilenIsSayisi);
        //    }
        //}
        public int Son30GunIcindeTeslimEdilenIsSayisi()
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                //MailGonder();
                return db.spGecenOtuzGunIcindeBitirilenIsler().FirstOrDefault().Value;
            }
        }

        public ActionResult _vGecenOtuzGunIcindeBitirilenIslerListe()
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                var list = db.spGecenOtuzGunIcindeBitirilenIslerListe().ToList();
                return View(list);
            }
        }


        #endregion inanc

       }
}
