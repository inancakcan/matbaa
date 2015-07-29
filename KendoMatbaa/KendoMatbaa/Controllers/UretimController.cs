using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.App_Start;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class UretimController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /Uretim/
        public ActionResult Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
                {
                    var uretim = db.Uretim.Include(u => u.AltUretimAsamalari).Include(u => u.IsEmri).Include(u => u.Personel1).OrderByDescending(Uretim => Uretim.UretimId).Take(100);
                    return View(uretim.ToList());
                }
                else
                {
                    //MatbaaAdmin değilse sadece kendi işlerin göster
                    var uretim = db.Uretim.Include(u => u.AltUretimAsamalari).Include(u => u.IsEmri).Include(u => u.Personel1).Where(s=>s.Personel==User.KullaniciUN).OrderByDescending(Uretim => Uretim.UretimId).Take(100);
                    return View(uretim.ToList());
                }
            }
            else
            {
                if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
                {
                    var uretim =
                        db.Uretim.Include(u => u.AltUretimAsamalari).Include(u => u.IsEmri).Include(u => u.Personel1);
                    uretim = uretim.Where(s => s.IsEmri.IsNo.ToUpper().Contains(searchString.ToUpper()) || s.IsEmriId.ToString().Contains(searchString.ToUpper()));
                    //.Where(s=>s.IsEmri.IsNo.ToUpper().Contains(searchString.ToUpper() || s.IsEmriId.ToString().Contains(searchString.ToUpper())));
                    return View(uretim.ToList());
                }
                else
                {
                    //MatbaaAdmin değilse sadece kendi işlerin göster
                    var uretim =
                     db.Uretim.Include(u => u.AltUretimAsamalari).Include(u => u.IsEmri).Include(u => u.Personel1).Where(s => s.Personel == User.KullaniciUN);
                    uretim = uretim.Where(s => s.IsEmri.IsNo.ToUpper().Contains(searchString.ToUpper()) || s.IsEmriId.ToString().Contains(searchString.ToUpper()));
                    //.Where(s=>s.IsEmri.IsNo.ToUpper().Contains(searchString.ToUpper() || s.IsEmriId.ToString().Contains(searchString.ToUpper())));
                    return View(uretim.ToList());
                }

            }
        }


        public ActionResult IseSimdiBasladim(int UretimId)
        {

            var uretim = (from urt in db.Uretim
                         where urt.UretimId == UretimId
                         select urt).First();
            uretim.UretimeBaslama = DateTime.Now;
            
            try
            {
                uretim.UretimeBaslama = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretim.IsEmriId, index = 8 })
                         .Success("Üretim aşamasını başarılı olarak 'işe başaladım' olarak işaretlediniz.");
            }
            catch (Exception e)
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = uretim.IsEmriId, index = 8 })
                    .Danger("Üretim aşamasını 'işe baladım' olarak işaretlemede hata oluştu.Hata:" + e.Message);
            }

        }



        public ActionResult IsiBitirdim(int UretimId)
        {
            Query q = new Query();
            q.IsiBitirdim(UretimId);
            //return View("Index");
            int iIsEmriId=q.UretiIddenIsEmriIdDon(UretimId);
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = iIsEmriId, index = 8 }).Success("İşi bitirdim olarak işaretleme başarılı..");
        }

        // GET: /Uretim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uretim uretim = db.Uretim.Find(id);
            if (uretim == null)
            {
                return HttpNotFound();
            }
            return View(uretim);
        }

        // GET: /Uretim/Create
        public ActionResult Create()
        {
            ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId", "AltUretimAsamalariAdi");
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            ViewBag.Personel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi");
            return View();
            //if (PersonelUretimAsamasiIcinYetkilendirimisMi(int.Parse(ViewBag.IsEmriId),))
            //{
            //    return View();
            //}
        }

        // POST: /Uretim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UretimId,IsEmriId,AltUretimAsamalariId,Tarih,Personel,UretimeBaslama,UretimiSonlandirma")] Uretim uretim)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                int IsEmriId = uretim.IsEmriId;
                int AltUretimAsamalariId = uretim.AltUretimAsamalariId;
                //Eger UretimYetkilendirmesi yapılmamıssa kaydedilmeyecek
                if (PersonelUretimAsamasiIcinYetkilendirimisMi(IsEmriId, AltUretimAsamalariId, UserManager.User.Username))
                {

                    if (ModelState.IsValid)
                    {
                        db.Uretim.Add(uretim);
                        db.SaveChanges();
                        //Burada mail gonderilecek

                        Query q = new Query();
                        string IlgiliAdi = q.IlgilininAdiniDon(uretim.IsEmriId);
                        string IlgilininEPostaAdresi = q.IlgilininEPostaAdresiniDon(uretim.IsEmriId);
                        string IsNo = q.IsEmriIddenIsNoDon(uretim.IsEmriId);
                        string AsamaAdi = q.AltUretimAsamalariIddenAsamaAdiDon(uretim.AltUretimAsamalariId);
                        string MailBody = "TSE Matbaasında yürütülen " + IsNo + " lu işiniz, " + DateTime.Now.ToString() +
                                          " itibarı ile," + AsamaAdi + " aşamasına geçmiştir.";
                        MailGonder("ukarkin@tse.org.tr", IlgilininEPostaAdresi, "TSE Matbaasında yürütülen " + IsNo + " lu işiniz", MailBody);
                        return RedirectToAction("Index", "Uretim").Success("Üretim aşamasını başarılı biçimde üzerinize aldınız.");
                    }

                    ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId",
                        "AltUretimAsamalariAdi", uretim.AltUretimAsamalariId);
                    ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretim.IsEmriId);
                    ViewBag.Personel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", uretim.Personel);
                    return View(uretim);
                }
                else
                {
                    //yetkilendirme yapılmamışsa nereye yonlendireceksen yonlendir..
                    return RedirectToAction("Create", "Uretim").Warning("Bu iş emri kapsamında ilgili üretim aşaması için yetkilendirilmemişsiniz. Uygulama yöneticiniz ile bağlantı kurup yetki isteyebilirsiniz.");
                    //return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Uretim").Warning("Yetkiniz yok!");
            }
        }

        // GET: /Uretim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uretim uretim = db.Uretim.Find(id);
            if (uretim == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId", "AltUretimAsamalariAdi", uretim.AltUretimAsamalariId);
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretim.IsEmriId);
            ViewBag.Personel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", uretim.Personel);
            return View(uretim);
        }

        // POST: /Uretim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UretimId,IsEmriId,AltUretimAsamalariId,Tarih,Personel,UretimeBaslama,UretimiSonlandirma")] Uretim uretim)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(uretim).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.AltUretimAsamalariId = new SelectList(db.AltUretimAsamalari, "AltUretimAsamalariId", "AltUretimAsamalariAdi", uretim.AltUretimAsamalariId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretim.IsEmriId);
                ViewBag.Personel = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", uretim.Personel);
                return View(uretim);
            }
            else
            {
                return RedirectToAction("Index", "Uretim").Warning("Yetkiniz yok!");
            }
        }

        // GET: /Uretim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uretim uretim = db.Uretim.Find(id);
            if (uretim == null)
            {
                return HttpNotFound();
            }
            return View(uretim);
        }

        // POST: /Uretim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uretim uretim = db.Uretim.Find(id);
            db.Uretim.Remove(uretim);
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


       
    }
}
