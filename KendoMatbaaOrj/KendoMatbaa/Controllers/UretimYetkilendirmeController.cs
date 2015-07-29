using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Infrastructure.Implementation;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;

namespace KendoMatbaa.Controllers
{
    public class UretimYetkilendirmeController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /UretimYetkilendirme/
        //public ActionResult Index()
        //{
        //    var uretimyetkilendirme = db.UretimYetkilendirme.Include(u => u.AltUretimAsamalari).Include(u => u.IsEmri).Include(u => u.Personel);
        //    return View(uretimyetkilendirme.ToList());
        //}



        public ActionResult Index(string searchString, bool? chkBanaAtanmislar, bool? chkDevamEdenler)
        {

            var uretimyetkilendirme = db.UretimYetkilendirme.Include(u => u.AltUretimAsamalari).Include(u => u.IsEmri).Include(u => u.Personel);
            if (!String.IsNullOrEmpty(searchString))
            {
                if (!chkBanaAtanmislar.Value && !chkDevamEdenler.Value)
                {
                    uretimyetkilendirme = uretimyetkilendirme
                            .Where(s => s.IsEmriId.ToString()
                            .Contains(searchString.ToUpper()) || s.IsEmri.IsNo.ToUpper().Contains(searchString.ToUpper()) || s.IsEmri.Adi.ToUpper().Contains(searchString.ToUpper())
                            );
                }
                else if (chkBanaAtanmislar.Value && !chkDevamEdenler.Value)
                {
                    uretimyetkilendirme = uretimyetkilendirme
                            .Where(s => s.IsEmriId.ToString()
                            .Contains(searchString.ToUpper()) || s.IsEmri.IsNo.ToUpper().Contains(searchString.ToUpper()) || s.IsEmri.Adi.ToUpper().Contains(searchString.ToUpper())
                             && (s.PersonelUN==UserManager.User.KullaniciUN)
                            );
                  
                }
                else if (!chkBanaAtanmislar.Value && chkDevamEdenler.Value)
                {
                    //uretimyetkilendirme = db.UretimYetkilendirme.Include(u => u.Uretim)
                    //        .Where(s => s.IsEmriId.ToString()
                    //        .Contains(searchString.ToUpper()) || s.IsEmri.IsNo.ToUpper().Contains(searchString.ToUpper()) || s.IsEmri.Adi.ToUpper().Contains(searchString.ToUpper())
                    //         && (s.PersonelUN == UserManager.User.KullaniciUN) 
                    //        );
                }

            }
            return View(uretimyetkilendirme.ToList());
        }
        public ActionResult UretimYetkilendirmeArama(string IsNo,   bool? chkBanaAtanmislar, bool? chkDevamEdenler)
        {
                using (matbaaEntities ent = new matbaaEntities())
                {
                    bool chkBanaAtanmislarSonuc = false;
                    bool chkDevamEdenlerSonuc = false;
                    string IsNoSonuc = string.Empty;
                    if (chkBanaAtanmislar != null)
                    {
                        if (chkBanaAtanmislar != false)
                        {
                            chkBanaAtanmislarSonuc = true;
                        }
                    }
                    if (chkDevamEdenler != null)
                    {
                        if (chkDevamEdenler != false)
                        {
                            chkDevamEdenlerSonuc = true;
                        }
                    }
                    if (IsNo != null)
                    {
                        IsNoSonuc = IsNo;
                    }

                    List<uspUretimYetkilendirmeArama_Result> oUretimYetkilendirmeArama =
                        new List<uspUretimYetkilendirmeArama_Result>();
                    //oUretimYetkilendirmeArama = ent.uspUretimYetkilendirmeArama(User.KullaniciUN, IsNo, true).ToList();
                    if (!String.IsNullOrEmpty(IsNoSonuc))
                    {
                        if (chkBanaAtanmislarSonuc && !chkDevamEdenlerSonuc)
                        {
                            oUretimYetkilendirmeArama =
                                ent.uspUretimYetkilendirmeArama(User.KullaniciUN, IsNo, null).ToList();
                        }
                        if (!chkBanaAtanmislarSonuc && chkDevamEdenlerSonuc)
                        {
                            oUretimYetkilendirmeArama = ent.uspUretimYetkilendirmeArama(null, IsNo, true).ToList();
                        }
                        if (chkBanaAtanmislarSonuc && chkDevamEdenlerSonuc)
                        {
                            oUretimYetkilendirmeArama =
                                ent.uspUretimYetkilendirmeArama(User.KullaniciUN, IsNo, true).ToList();
                        }
                        if (!chkBanaAtanmislarSonuc && !chkDevamEdenlerSonuc)
                        {
                            oUretimYetkilendirmeArama = ent.uspUretimYetkilendirmeArama(null, IsNo, null).ToList();
                        }

                    }
                    //else
                    //{
                    //    if (chkBanaAtanmislar.Value && !chkDevamEdenler.Value)
                    //    {
                    //        oUretimYetkilendirmeArama = ent.uspUretimYetkilendirmeArama(User.KullaniciUN, null, false).ToList();
                    //    }
                    //    if (!chkBanaAtanmislar.Value && chkDevamEdenler.Value)
                    //    {
                    //        oUretimYetkilendirmeArama = ent.uspUretimYetkilendirmeArama(null, null, true).ToList();
                    //    }
                    //    if (chkBanaAtanmislar.Value && chkDevamEdenler.Value)
                    //    {
                    //        oUretimYetkilendirmeArama = ent.uspUretimYetkilendirmeArama(User.KullaniciUN, null, true).ToList();
                    //    }
                    //}
                    return View(oUretimYetkilendirmeArama);
                }
        }



        public ActionResult _BelirliIsEmrininYetkilendirmeleri(int IsEmriId)
        {
            var uretimyetkilendirme =
                db.UretimYetkilendirme.Include(u => u.AltUretimAsamalari)
                    .Include(u => u.IsEmri)
                    .Include(u => u.Personel)
                    .OrderByDescending(u=>u.Personel.PersonelAdiSoyadi)
                    .Where(i => i.IsEmriId == IsEmriId);
            return View(uretimyetkilendirme.ToList());
        }

        // GET: /UretimYetkilendirme/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimYetkilendirme uretimyetkilendirme = db.UretimYetkilendirme.Find(id);
            if (uretimyetkilendirme == null)
            {
                return HttpNotFound();
            }
            return View(uretimyetkilendirme);
        }

        // GET: /UretimYetkilendirme/Create
        public ActionResult Create(string SeciliIsEmriId)
        {
            ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId", "AltUretimAsamalariAdi");
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", SeciliIsEmriId);
            ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi");
            return View();
        }

        // POST: /UretimYetkilendirme/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UretimYetkilendirmeId,PersonelUN,AltUretimAsamalariId,IsEmriId,Aciklama")] UretimYetkilendirme uretimyetkilendirme)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.UretimYetkilendirme.Add(uretimyetkilendirme);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    //Burada iş emri parçası için yetkilendirilen personele mail gitmesi gerekli

                    Query q = new Query();
                    string PersonelAdiSoyadi = q.PersonelUNdenPeronelAdiSoyadiniDon(uretimyetkilendirme.PersonelUN);
                    string YetkilendirilenPersonelinEPostaAdresi = q.PersonelUNdenPeronelMailiniDon(uretimyetkilendirme.PersonelUN);
                    string IsNo = q.IsEmriIddenIsNoDon(uretimyetkilendirme.IsEmriId);
                    string Subject = IsNo + " nolu iş emri için yetkilendirme mesajıdır";
                    string AltUretimAsamasiAdi = q.AltUretimAsamalariIddenAsamaAdiDon(uretimyetkilendirme.AltUretimAsamalariId);
                    string Body = "Sn. " + PersonelAdiSoyadi + ",<br>" + IsNo + " nolu iş emri üzerinde, " +
                                  AltUretimAsamasiAdi + " üretim aşaması için yetkilendirildiniz. İyi çalışmalar.";
                    MailGonder("ukarkin@tse.org.tr", YetkilendirilenPersonelinEPostaAdresi, Subject, Body);

                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretimyetkilendirme.IsEmriId, index = 8 }).Success("Yetki ekleme işlemi başarılı");
                }

                ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId", "AltUretimAsamalariAdi", uretimyetkilendirme.AltUretimAsamalariId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretimyetkilendirme.IsEmriId);
                ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", uretimyetkilendirme.PersonelUN);
                return View(uretimyetkilendirme);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretimyetkilendirme.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }

        // GET: /UretimYetkilendirme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimYetkilendirme uretimyetkilendirme = db.UretimYetkilendirme.Find(id);
            if (uretimyetkilendirme == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId", "AltUretimAsamalariAdi", uretimyetkilendirme.AltUretimAsamalariId);
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretimyetkilendirme.IsEmriId);
            ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", uretimyetkilendirme.PersonelUN);
            return View(uretimyetkilendirme);
        }

        // POST: /UretimYetkilendirme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UretimYetkilendirmeId,PersonelUN,AltUretimAsamalariId,IsEmriId,Aciklama")] UretimYetkilendirme uretimyetkilendirme)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(uretimyetkilendirme).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretimyetkilendirme.IsEmriId, index = 8 }).Success("Yetki güncelleme işlemi başarılı");
                }
                ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId", "AltUretimAsamalariAdi", uretimyetkilendirme.AltUretimAsamalariId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretimyetkilendirme.IsEmriId);
                ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", uretimyetkilendirme.PersonelUN);
                return View(uretimyetkilendirme);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretimyetkilendirme.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }

        // GET: /UretimYetkilendirme/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimYetkilendirme uretimyetkilendirme = db.UretimYetkilendirme.Find(id);
            if (uretimyetkilendirme == null)
            {
                return HttpNotFound();
            }
            return View(uretimyetkilendirme);
        }

        // POST: /UretimYetkilendirme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UretimYetkilendirme uretimyetkilendirme = db.UretimYetkilendirme.Find(id);
            db.UretimYetkilendirme.Remove(uretimyetkilendirme);
            db.SaveChanges();


            //Burada iş emri parçası için yetkisi geri alınan personele mail gitmesi gerekli

            Query q = new Query();
            string PersonelAdiSoyadi = q.PersonelUNdenPeronelAdiSoyadiniDon(uretimyetkilendirme.PersonelUN);
            string YetkilendirilenPersonelinEPostaAdresi = q.PersonelUNdenPeronelMailiniDon(uretimyetkilendirme.PersonelUN);
            string IsNo = q.IsEmriIddenIsNoDon(uretimyetkilendirme.IsEmriId);
            string Subject = IsNo + " nolu iş emri için yetkilendirmenin geri alınmasına ilişkin mesajıdır";
            string AltUretimAsamasiAdi = q.AltUretimAsamalariIddenAsamaAdiDon(uretimyetkilendirme.AltUretimAsamalariId);
            string Body = "Sn. " + PersonelAdiSoyadi + ",<br>" + IsNo + " nolu iş emri üzerinde, " +
                          AltUretimAsamasiAdi + " üretim aşaması için yetki geri alınmıştır. İyi çalışmalar.";
            MailGonder("ukarkin@tse.org.tr", YetkilendirilenPersonelinEPostaAdresi, Subject, Body);


            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretimyetkilendirme.IsEmriId, index = 8 }).Success("Yetki silme işlemi başarılı");
        }


        public ActionResult  IseSimdiBasladim(int UretimYetkilendirmeId)
        {
         
            var uretimYetkilendirme = (from c in db.UretimYetkilendirme
                                       where c.UretimYetkilendirmeId == UretimYetkilendirmeId
                                       select c).First();

            Uretim uretim = (from c in db.Uretim
                             where c.IsEmriId == uretimYetkilendirme.IsEmriId
                             && c.AltUretimAsamalariId == uretimYetkilendirme.AltUretimAsamalariId
                             && c.Personel == uretimYetkilendirme.PersonelUN
                             select c).First();




            try
            {
                uretim.UretimeBaslama = DateTime.Now;
                db.SaveChanges();
               return   RedirectToAction("TabView", "IsEmri", new {IsEmriId = uretimYetkilendirme.IsEmriId, index = 8})
                        .Success("Üretim aşamasını başarılı olarak 'işe başaladım' olarak işaretlediniz.");
            }
            catch(Exception e)
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretimYetkilendirme.IsEmriId, index = 8 })
                    .Danger("Üretim aşamasını 'işe baladım' olarak işaretlemede hata oluştu.Hata:"+e.Message);
            }
            
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
