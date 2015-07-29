using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevExpress.XtraRichEdit.Services.Implementation;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;
namespace KendoMatbaa.Controllers
{
    public class TeslimatRaporuController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /TeslimatRaporu/
        public ActionResult Index(string IsNo)
        {
            /*
            var teslimatraporu = db.TeslimatRaporu.Include(t => t.IsEmri).Include(t => t.Personel);
            return View(teslimatraporu.ToList());
             */

            var teslimatraporu = db.TeslimatRaporu.Include(t => t.IsEmri).Include(t => t.Personel);
            if (!String.IsNullOrEmpty(IsNo))
            {
                teslimatraporu = teslimatraporu.Where(s => s.IsEmri.IsNo.ToString().Contains(IsNo.ToUpper())
                    || s.IsEmri.Adi.ToUpper().Contains(IsNo.ToUpper()) 
                    || s.Personel.PersonelAdiSoyadi.ToUpper().Contains(IsNo.ToUpper())
                    || s.TeslimAlan.ToUpper().Contains(IsNo.ToUpper())
                    ); 
            

            using(matbaaEntities ent=new matbaaEntities())
            {
                Query q = new Query();
                int IsEmriId = q.IsNodanIsEmriIdDon(IsNo);
                ViewBag.IsEmriId = IsEmriId;
                int ToplamTeslimEdilmesiGereken=ent.spTeslimatOrani(IsEmriId).First().ToplamTeslimEdilmesiGereken;
                int TeslimEdilen=ent.spTeslimatOrani(IsEmriId).First().ToplamTeslimEdilen;
                string TeslimatOrani=ent.spTeslimatOrani(IsEmriId).First().TeslimatOrani;


                int TeslimEdilmesiGerekenToplamKalan = ToplamTeslimEdilmesiGereken - TeslimEdilen;

                Session["TeslimEdilmesiGerekenToplamKalan"] = TeslimEdilmesiGerekenToplamKalan;
                if (TeslimEdilmesiGerekenToplamKalan != 0)
                {
                    ViewBag.TumunuTeslimEtGorunecekMi = true;
                }
                else
                {
                    ViewBag.TumunuTeslimEtGorunecekMi = false;
                }
                //int Oran = TeslimEdilen/ToplamTeslimEdilmesiGereken;
                ViewBag.TeslimatOraniBilgisi = TeslimEdilen.ToString() + "/" + ToplamTeslimEdilmesiGereken + " (Teslimat Oranı=" + TeslimatOrani + ")";
               
                Session["ToplamTeslimEdilmesiGereken"] = ToplamTeslimEdilmesiGereken;
                Session["TeslimEdilen"] = TeslimEdilen;
            }
}

            return View(teslimatraporu.ToList().Take(50));
 
        }




        public ActionResult TumunuTeslimEt(int IsEmriId)
        {
            Query q = new Query();
            if (IsEmriId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                using (matbaaEntities ent = new matbaaEntities())
            {
                //int ToplamTeslimEdilmesiGereken = ent.spTeslimatOrani(IsEmriId).First().ToplamTeslimEdilmesiGereken;
                int ToplamTeslimEdilmesiGereken = Convert.ToInt32(Session["TeslimEdilmesiGerekenToplamKalan"].ToString());
                TeslimatRaporu teslimatRaporu=new TeslimatRaporu();
                teslimatRaporu.IsEmriId = IsEmriId;
                
                teslimatRaporu.TeslimEdenPersonel = User.KullaniciUN;
                teslimatRaporu.TeslimAlan = "-";
                teslimatRaporu.TeslimatMiktari = ToplamTeslimEdilmesiGereken;
                teslimatRaporu.Tarih=DateTime.Now;
                ent.TeslimatRaporu.Add(teslimatRaporu);
                ent.SaveChanges();
                    //return View("Index").Success("İşin tümü başarı ile teslim edildi");
                    
                return RedirectToAction("Index", "TeslimatRaporu",new {IsNo=q.IsEmriIddenIsNoDon(IsEmriId)}).Danger("İşin tümü başarı ile teslim edildi");
            }
            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "TeslimatRaporu", new { IsNo = q.IsEmriIddenIsNoDon(IsEmriId) }).Danger("İş teslimatı başarısız. Hata:" + e.Message);
            }
            
        }




        // GET: /TeslimatRaporu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeslimatRaporu teslimatraporu = db.TeslimatRaporu.Find(id);
            if (teslimatraporu == null)
            {
                return HttpNotFound();
            }
            return View(teslimatraporu);
        }

        // GET: /TeslimatRaporu/Create
        public ActionResult Create(string SeciliIsEmriId)
        {

            using (matbaaEntities ent = new matbaaEntities())
            {
                
                Query q = new Query();
                ViewBag.TeslimTarihi = q.TaahhutEdilenTeslimTarihiniDon(Convert.ToInt32(SeciliIsEmriId)).Replace(" 00:00:00","");
                int IsEmriId = Convert.ToInt32(SeciliIsEmriId);
                ViewBag.IsEmriId = IsEmriId;
                int ToplamTeslimEdilmesiGereken = ent.spTeslimatOrani(IsEmriId).First().ToplamTeslimEdilmesiGereken;
                int TeslimEdilen = ent.spTeslimatOrani(IsEmriId).First().ToplamTeslimEdilen;
                string TeslimatOrani = ent.spTeslimatOrani(IsEmriId).First().TeslimatOrani;
                ViewBag.TeslimatOraniBilgisi = TeslimEdilen.ToString() + "/" + ToplamTeslimEdilmesiGereken + " (Teslimat Oranı=" + TeslimatOrani + ")";

                Session["ToplamTeslimEdilmesiGereken"] = ToplamTeslimEdilmesiGereken;
                Session["TeslimEdilen"] = TeslimEdilen;
            }



            ViewBag.IsEmriId = new SelectList(db.IsEmri.OrderByDescending(a => a.IsEmriId), "IsEmriId", "IsNo", SeciliIsEmriId);
            ViewBag.TeslimEdenPersonel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi");
            return View();
        }

        // POST: /TeslimatRaporu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeslimatRaporuId,IsEmriId,TeslimEdenPersonel,TeslimAlan,TeslimatMiktari,Tarih")] TeslimatRaporu teslimatraporu, int TeslimatMiktari)
        {
            if (TeslimatMiktari > int.Parse(Session["ToplamTeslimEdilmesiGereken"].ToString()))
            {
                int TeslimEdilebilecekMaxMiktar = int.Parse(Session["ToplamTeslimEdilmesiGereken"].ToString()) -
                                                  int.Parse(Session["TeslimEdilen"].ToString());
                int SeciliIsEmriId = int.Parse(Request["SeciliIsEmriId"].ToString());
                return RedirectToAction("Create", new { SeciliIsEmriId = SeciliIsEmriId }).Warning("Teslim edebileceğiniz maksimum miktar="+TeslimEdilebilecekMaxMiktar.ToString()+"! Bu iş için daha fazla miktarda teslimat yapamazsınız!");
            }
            if (ModelState.IsValid)
            {
                db.TeslimatRaporu.Add(teslimatraporu);
                db.SaveChanges();
                Session.Remove("ToplamTeslimEdilmesiGereken");
                Session.Remove("TeslimEdilen");
                Query q=new Query();
                string IlgiliIsNo = q.IsEmriIddenIsNoDon(teslimatraporu.IsEmriId.Value);
                return RedirectToAction("Index", new { IsNo = IlgiliIsNo }).Success("Teslimat raporu başarı ile eklendi.");
            }

            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", teslimatraporu.IsEmriId);
            ViewBag.TeslimEdenPersonel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", teslimatraporu.TeslimEdenPersonel);
            return View(teslimatraporu);
        }

        // GET: /TeslimatRaporu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeslimatRaporu teslimatraporu = db.TeslimatRaporu.Find(id);
            if (teslimatraporu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", teslimatraporu.IsEmriId);
            ViewBag.TeslimEdenPersonel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", teslimatraporu.TeslimEdenPersonel);
            return View(teslimatraporu);
        }

        // POST: /TeslimatRaporu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TeslimatRaporuId,IsEmriId,TeslimEdenPersonel,TeslimAlan,TeslimatMiktari,Tarih")] TeslimatRaporu teslimatraporu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teslimatraporu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index").Success("Güncelleme başarılı");
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", teslimatraporu.IsEmriId);
            ViewBag.TeslimEdenPersonel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", teslimatraporu.TeslimEdenPersonel);
            return View(teslimatraporu);
        }

        // GET: /TeslimatRaporu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeslimatRaporu teslimatraporu = db.TeslimatRaporu.Find(id);
            if (teslimatraporu == null)
            {
                return HttpNotFound();
            }
            return View(teslimatraporu);
        }

        // POST: /TeslimatRaporu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Query q = new Query();
            TeslimatRaporu teslimatraporu = db.TeslimatRaporu.Find(id);
            string IlgiliIsNo = q.IsEmriIddenIsNoDon(teslimatraporu.IsEmriId.Value);
            db.TeslimatRaporu.Remove(teslimatraporu);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Index", new { IsNo = IlgiliIsNo }).Success("Teslimat raporu başarı ile silindi.");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
