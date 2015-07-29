using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.App_Start;
using KendoMatbaa.IKBSServis;
using KendoMatbaa.Models;
using DevExpress.Web.Mvc;

namespace KendoMatbaa.Controllers
{
    public class IsEmrisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmris
        public ActionResult Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                var isEmri = db.IsEmri.Include(i => i.Birim).OrderByDescending(IsEmri => IsEmri.IsEmriId).Take(20).Where(s => s.Silindi == false);
                return View(isEmri.ToList());
            }

            else
            {
                var isEmri = db.IsEmri.Include(i => i.Birim);
                //isEmri = isEmri.Where(s => s.IsEmriId.ToString().Contains(searchString.ToUpper()) && s.Silindi==false|| s.IsNo.ToUpper().Contains(searchString.ToUpper()) || s.Adi.ToUpper().Contains(searchString.ToUpper()) || s.Birim.BirimAdi.ToUpper().Contains(searchString.ToUpper()) && s.Silindi==false);
                isEmri = isEmri.Where(s => s.IsEmriId.ToString().Contains(searchString.ToUpper()) || s.IsNo.ToUpper().Contains(searchString.ToUpper()) || s.Adi.ToUpper().Contains(searchString.ToUpper()) ||  s.IKBSBirim.Adi.ToUpper().Contains(searchString.ToUpper()) && s.Silindi == false);
                isEmri = isEmri.Where(s => s.Silindi == false); return View(isEmri.ToList());
            }}
        // GET: IsEmris/Details/5
        public ActionResult Details(int? id)
        {
            Response.Redirect("../../IsEmri/TabView?IsEmriId=" + id.Value.ToString());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isEmri = db.IsEmri.Find(id);
            if (isEmri == null)
            {
                return HttpNotFound();
            }
            return View(isEmri);
        }


        //public ActionResult Create2()
        //{
        //    return View();
        //}


        // GET: IsEmris/Create
        public ActionResult Create()
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                ViewBag.BitmisIsBoyutlariId = new SelectList(db.BitmisIsBoyutlari, "Id", "BitmisIsBoyutuAdi");
                ViewBag.OtomatikAlinmisIsNo = OtomatikIsNoAl();
                //ViewBag.BirimId = new SelectList(db.Birim.OrderBy(x => x.BirimAdi), "BirimId", "BirimAdi");
                using (IKBSServis.ikbsSoapClient g = new ikbsSoapClient())
                {
                    DataSet dsBirimListesi = g.BirimListesi();
                    ViewBag.BirimUN = new SelectList(db.IKBSBirim.OrderBy(x => x.Adi), "BirimUN", "Adi");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "IsEmri").Warning("Yeni İş Emri bilgisi ekleme yetkisine sahip değilsiniz!");
            }
            //ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi");
            //return View();
        }

        // POST: IsEmris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriId,IsNo,TarihSayi,Adi,Personel,Telefon,KabulTarihi,TeslimTarihi,Aciklama,BirimId,Silindi,IlgiliEPosta,IKBSBirimUN,BitmisIsBoyutlariId")] IsEmri isEmri, string BirimUN, IEnumerable<string> OlmayanIsParcalariMultiSelect)
        {
            Query q=new Query();
            ModelState.Remove("Silindi");
            if (ModelState.IsValid)
            {
                //db.IsEmri.Add(isEmri);
                //db.SaveChanges();
                //return RedirectToAction("Index");


                //ilk eklemeyi yaparken silindiyi false a cek
                isEmri.Silindi = false;
                isEmri.IKBSBirimUN = Guid.Parse(BirimUN);
                isEmri.IsNo = OtomatikIsNoAl();
                db.IsEmri.Add(isEmri);
                db.SaveChanges();
                var olmayanIsParcalariMultiSelect = OlmayanIsParcalariMultiSelect as string[] ?? OlmayanIsParcalariMultiSelect.ToArray();
                foreach (var isParcasi in olmayanIsParcalariMultiSelect)
                {
                   bool bak= q.IsEmriYokIsParcasiTablosunaEkle(isEmri.IsEmriId, isParcasi.ToString());
                }
                //tam burada  diğer 4 tabloyada gelecek düzenlemeler için insert yapmalıyız
                //spIsEmriInsertEderkenDigerDortTabloyadaInsertEt(IsEmriId) prosedürünü çalıştırmak gerekli
                using (matbaaEntities ent = new matbaaEntities())
                {
                    ent.spIsEmriInsertEderkenDigerDortTabloyadaInsertEt(isEmri.IsEmriId);

                }
                MailGonder("noreply@tse.org.tr", ConfigurationManager.AppSettings["YeniIsEmrindeMailGonderilecekler"], isEmri.IsNo + " iş nolu yeni iş emri hakkında",
                    "Sisteme," + isEmri.IsNo + " iş nolu yeni bir iş emri girilmiştir.");
                return RedirectToAction("Index", "IsEmris").Success("Yeni İş Emri bigisi ekleme işlemi başarılı");


            }

            ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi", isEmri.BirimId);
            return RedirectToAction("Index", "IsEmris").Danger("Yeni İş Emri bilgisi ekleme işlemi başarısız");
        }

        // GET: IsEmris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IsEmri isEmri = db.IsEmri.Find(id);
                if (isEmri == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi", isEmri.BirimId);
                using (IKBSServis.ikbsSoapClient g = new ikbsSoapClient())
                {
                    DataSet dsBirimListesi = g.BirimListesi();
                    ViewBag.BirimUN = new SelectList(db.IKBSBirim.OrderBy(x => x.Adi), "BirimUN", "Adi");
                }
                return View(isEmri);
            }
            else
            {
                return RedirectToAction("Index", "IsEmri").Warning("Yeni İş Emri bilgisi güncelleme yetkisine sahip değilsiniz!");
            }
        }

        // POST: IsEmris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriId,IsNo,TarihSayi,Adi,Personel,Telefon,KabulTarihi,TeslimTarihi,Aciklama,BirimId,Silindi,IlgiliEPosta,IKBSBirimUN")] IsEmri isEmri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isEmri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirimId = new SelectList(db.Birim.OrderBy(x => x.BirimAdi), "BirimId", "BirimAdi", isEmri.BirimId);
            return View(isEmri);
        }

        // GET: IsEmris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isEmri = db.IsEmri.Find(id);
            if (isEmri == null)
            {
                return HttpNotFound();
            }
            return View(isEmri);
        }

        // POST: IsEmris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmri isEmri = db.IsEmri.Find(id);
            db.IsEmri.Remove(isEmri);
            db.SaveChanges();
            return RedirectToAction("Index", "IsEmri").Success(" İş Emri bigisi silme işlemi başarılı");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public bool IsEmriBilgigirisiTamamMi(int IsEmriId)
        {
            bool Sonuc = false;
            Sonuc = db.spIsEmriBilgiGirisiTamamlanmismi(IsEmriId).FirstOrDefault().TamamMi.Value;
            return Sonuc;

        }

        //public JsonResult OlmayanIsParcalariniGetir()
        //{
        //    Query q = new Query();
        //    //var bka = db.YokIsParcasi.ToList();
        //    //return Json(db.YokIsParcasi.ToList(), JsonRequestBehavior.AllowGet);}
        //    return Json(q.YokIsParcasiDon(), JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult _YokIsParcalari()
        //{
        //    //return PartialView("_YokIsParcalari");
        //    //Query q = new Query();
        //    return PartialView();
        //}
    }
}
