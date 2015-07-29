using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;

namespace KendoMatbaa.Controllers
{
    public class IsEmriKullanilacakKagitsController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriKullanilacakKagits
        public ActionResult Index()
        {
            var isEmriKullanilacakKagit = db.IsEmriKullanilacakKagit.Include(i => i.AltGruplar).Include(i => i.AltGruplar1).Include(i => i.IsEmri).Include(i => i.BaskiEbatlari).Include(i => i.BaskiEbatlari1).Include(i => i.TabakaKesimleri).Include(i => i.TabakaKesimleri1);
            return View(isEmriKullanilacakKagit.ToList());
        }

        public ActionResult _BelirliIsEmrininKullanilanKagitlari(int IsEmriId)
        {
            var isEmriKullanilacakKagit = db.IsEmriKullanilacakKagit.Include(i => i.AltGruplar).Include(i => i.AltGruplar1).Include(i => i.IsEmri).Include(i => i.BaskiEbatlari).Include(i => i.BaskiEbatlari1).Include(i => i.TabakaKesimleri).Where(i => i.IsEmriID == IsEmriId);
            return View(isEmriKullanilacakKagit.ToList());
        }

        // GET: IsEmriKullanilacakKagits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKullanilacakKagit isEmriKullanilacakKagit = db.IsEmriKullanilacakKagit.Find(id);
            if (isEmriKullanilacakKagit == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKullanilacakKagit);
        }

        // GET: IsEmriKullanilacakKagits/Create
        public ActionResult Create(string SeciliIsEmriId)
        {
            //ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 47), "AltGruplarId", "AltGrupAdi");
            ViewBag.IcKagitCins = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 42).OrderBy(AltGruplar=>AltGruplar.AltGrupAdi), "AltGruplarId", "AltGrupAdi");
            ViewBag.KapakCins = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 42).OrderBy(AltGruplar => AltGruplar.AltGrupAdi), "AltGruplarId", "AltGrupAdi");
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", SeciliIsEmriId);
            ViewBag.SeciliIsEmriId = SeciliIsEmriId;
            
            /*orj
            ViewBag.KapakBaskiBoyutu = new SelectList(db.KesimBoyutlari, "BaskiEbatlariId", "KesimBoyutlari1");
            ViewBag.IcBaskiBoyutu = new SelectList(db.KesimBoyutlari, "BaskiEbatlariId", "KesimBoyutlari1");
            orj*/

            ViewBag.KapakBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut");
            ViewBag.IcBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut");

            
            
            ViewBag.IcTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi");
            ViewBag.KapakTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi");
            return View();
        }

        // POST: IsEmriKullanilacakKagits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriID,IcKagitCins,IcKagitNet,IcKagitFire,IcBaskiBoyutu,IcTabaka,KapakCins,KapakBaskiBoyutu,KapakNet,KapakTabaka,KapakFire,Otokopi,IsEmriKullanilacakKagitId,Silindi")] IsEmriKullanilacakKagit isEmriKullanilacakKagit)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.IsEmriKullanilacakKagit.Add(isEmriKullanilacakKagit);
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilacakKagit.IsEmriID, index = 4 }).Success("Kullanılacak kağıt  bilgisi ekleme işlemi başarılı");
                }

                ViewBag.IcKagitCins = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", isEmriKullanilacakKagit.IcKagitCins);
                ViewBag.KapakCins = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", isEmriKullanilacakKagit.KapakCins);
                ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKullanilacakKagit.IsEmriID);
                ViewBag.KapakBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "BaskiEbatlari1", isEmriKullanilacakKagit.KapakBaskiBoyutu);
                ViewBag.IcBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "BaskiEbatlari1", isEmriKullanilacakKagit.IcBaskiBoyutu);
                ViewBag.IcTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi", isEmriKullanilacakKagit.IcTabaka);
                ViewBag.KapakTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi", isEmriKullanilacakKagit.KapakTabaka);
                return View(isEmriKullanilacakKagit);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilacakKagit.IsEmriID }).Warning("Yetkiniz yok!");
            }

        }

        // GET: IsEmriKullanilacakKagits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKullanilacakKagit isEmriKullanilacakKagit = db.IsEmriKullanilacakKagit.Find(id);
            if (isEmriKullanilacakKagit == null)
            {
                return RedirectToAction("Index", "IsEmri").Warning("Bu iş emri için kullanılacak kağıt  bilgisi bulunamadı!");
                //return HttpNotFound();
            }

            ViewBag.IcKagitCins = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 42).OrderBy(AltGruplar => AltGruplar.AltGrupAdi), "AltGruplarId", "AltGrupAdi", isEmriKullanilacakKagit.IcKagitCins);
            ViewBag.KapakCins = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 42).OrderBy(AltGruplar => AltGruplar.AltGrupAdi), "AltGruplarId", "AltGrupAdi", isEmriKullanilacakKagit.KapakCins);
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKullanilacakKagit.IsEmriID);
            ViewBag.KapakBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriKullanilacakKagit.KapakBaskiBoyutu);
            ViewBag.IcBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriKullanilacakKagit.IcBaskiBoyutu);
            ViewBag.IcTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi", isEmriKullanilacakKagit.IcTabaka);
            ViewBag.KapakTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi", isEmriKullanilacakKagit.KapakTabaka);
            return View(isEmriKullanilacakKagit);
        }

        // POST: IsEmriKullanilacakKagits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriID,IcKagitCins,IcKagitNet,IcKagitFire,IcBaskiBoyutu,IcTabaka,KapakCins,KapakBaskiBoyutu,KapakNet,KapakTabaka,KapakFire,Otokopi,IsEmriKullanilacakKagitId,Silindi")] IsEmriKullanilacakKagit isEmriKullanilacakKagit)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(isEmriKullanilacakKagit).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index", "IsEmri").Success("Kullanılacak kağıt  bilgisi güncelleme işlemi başarılı");
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilacakKagit.IsEmriID, index = 4 }).Success("Kullanılacak kağıt  bilgisi güncelleme işlemi başarılı");
                }
                ViewBag.IcKagitCins = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", isEmriKullanilacakKagit.IcKagitCins);
                ViewBag.KapakCins = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", isEmriKullanilacakKagit.KapakCins);
                ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKullanilacakKagit.IsEmriID);
                ViewBag.KapakBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "BaskiEbatlari1", isEmriKullanilacakKagit.KapakBaskiBoyutu);
                ViewBag.IcBaskiBoyutu = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "BaskiEbatlari1", isEmriKullanilacakKagit.IcBaskiBoyutu);
                ViewBag.IcTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi", isEmriKullanilacakKagit.IcTabaka);
                ViewBag.KapakTabaka = new SelectList(db.TabakaKesimleri, "TabakaKesimleriId", "TabakaKesimi", isEmriKullanilacakKagit.KapakTabaka);
                return View(isEmriKullanilacakKagit);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilacakKagit.IsEmriID }).Warning("Yetkiniz yok!");
            }
        }

        // GET: IsEmriKullanilacakKagits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKullanilacakKagit isEmriKullanilacakKagit = db.IsEmriKullanilacakKagit.Find(id);
            if (isEmriKullanilacakKagit == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKullanilacakKagit);
        }

        // POST: IsEmriKullanilacakKagits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriKullanilacakKagit isEmriKullanilacakKagit = db.IsEmriKullanilacakKagit.Find(id);
            db.IsEmriKullanilacakKagit.Remove(isEmriKullanilacakKagit);
            db.SaveChanges();
            //return RedirectToAction("Index", "IsEmri").Success("Kullanılacak kağıt  bilgisi silme işlemi başarılı");
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilacakKagit.IsEmriID, index = 4 }).Success("Kullanılacak kağıt  bilgisi silme işlemi başarılı");
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
