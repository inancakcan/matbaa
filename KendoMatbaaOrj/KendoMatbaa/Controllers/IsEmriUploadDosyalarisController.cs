using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.App_Start;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class IsEmriUploadDosyalarisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriUploadDosyalaris
        public ActionResult Index()
        {
            var isEmriUploadDosyalari = db.IsEmriUploadDosyalari.Include(i => i.IsEmri);
            return View(isEmriUploadDosyalari.ToList());
        }


        public ActionResult _BelirliIsEmrininDosyalariniDon( int IsEmriId)
        {
            ViewBag.IsEmriId = IsEmriId;
            var BelirliIsEmrininUploadDosyalari = db.IsEmriUploadDosyalari.Include(i => i.IsEmri).Where(i=>i.IsEmriId==IsEmriId);
            return View(BelirliIsEmrininUploadDosyalari.ToList());
        }

        // GET: IsEmriUploadDosyalaris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriUploadDosyalari isEmriUploadDosyalari = db.IsEmriUploadDosyalari.Find(id);
            if (isEmriUploadDosyalari == null)
            {
                return HttpNotFound();
            }
            return View(isEmriUploadDosyalari);
        }

        // GET: IsEmriUploadDosyalaris/Create
        public ActionResult Create()
        {
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            return View();
        }

        // POST: IsEmriUploadDosyalaris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriUploadDosyalariId,IsEmriId,DosyaAdi,DosyaAciklamasi,DosyaKonumu")] IsEmriUploadDosyalari isEmriUploadDosyalari)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.IsEmriUploadDosyalari.Add(isEmriUploadDosyalari);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriUploadDosyalari.IsEmriId);
                return View(isEmriUploadDosyalari);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriUploadDosyalari.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }

        // GET: IsEmriUploadDosyalaris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriUploadDosyalari isEmriUploadDosyalari = db.IsEmriUploadDosyalari.Find(id);
            if (isEmriUploadDosyalari == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriUploadDosyalari.IsEmriId);
            return View(isEmriUploadDosyalari);
        }

        // POST: IsEmriUploadDosyalaris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriUploadDosyalariId,IsEmriId,DosyaAdi,DosyaAciklamasi,DosyaKonumu")] IsEmriUploadDosyalari isEmriUploadDosyalari)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(isEmriUploadDosyalari).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriUploadDosyalari.IsEmriId);
                return View(isEmriUploadDosyalari);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriUploadDosyalari.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }


        //public bool DosyayiSil(string filePath)
        //{
        //    bool Basarim = false;
        //    try
        //    {
        //        string fullPath = Request.MapPath(filePath.Replace("c:\\Projects\\KendoMatbaa\\KendoMatbaa", "~"));
        //        if (System.IO.File.Exists(fullPath))
        //        {
        //            System.IO.File.Delete(fullPath);
        //        }
        //        Basarim = true;
        //        //return RedirectToAction("Index", "IsEmri").Success("Dosya silme işlemi başarılı");
        //    }
        //    catch (Exception)
        //    {
        //        Basarim = false;
                
        //    }
        //    return Basarim;
        //}

        public ActionResult DosyayiSil(string filePath,int isEmriUploadDosyalariId)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                try
                {
                    Query q=new Query();
                    int IsEmriIdd = q.isEmriUploadDosyalariIddenIsEmriIdDon(isEmriUploadDosyalariId);
                    string fullPath = Request.MapPath(filePath.Replace("c:\\Projects\\KendoMatbaa\\KendoMatbaa", "~"));
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        //Burada o dosyayı veritabanından da silmek gerekiyor
                        using (matbaaEntities ent = new matbaaEntities())
                        {
                            ent.spIsEmriDosyasiniSil(isEmriUploadDosyalariId);
                        }
                    }
                    //return RedirectToAction("Index", "IsEmri").Success("Dosya silme işlemi başarılı");
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriIdd, index = 7 }).Success("Dosya silme işlemi başarılı"); ;
                }
                catch (Exception)
                {
                    Query q = new Query();
                    int IsEmriIdd = q.isEmriUploadDosyalariIddenIsEmriIdDon(isEmriUploadDosyalariId);
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriIdd, index = 7 }).Danger("İş Emri bilgisi güncelleme işlemi başarısız");
                    

                }
            }
            else
            {
                return RedirectToAction("Index", "IsEmris").Danger("Yetkiniz yok!");
            }
        }


        // GET: IsEmriUploadDosyalaris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriUploadDosyalari isEmriUploadDosyalari = db.IsEmriUploadDosyalari.Find(id);
            if (isEmriUploadDosyalari == null)
            {
                return HttpNotFound();
            }
            return View(isEmriUploadDosyalari);
        }

        // POST: IsEmriUploadDosyalaris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriUploadDosyalari isEmriUploadDosyalari = db.IsEmriUploadDosyalari.Find(id);
            db.IsEmriUploadDosyalari.Remove(isEmriUploadDosyalari);
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
