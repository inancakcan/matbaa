using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class IsEmriEskiController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();


        public ActionResult Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                var isEmriEskii = db.IsEmriEski.OrderByDescending(isEmriEski => isEmriEski.ObjectID).Take(20);
                return View(isEmriEskii.ToList());
            }

            else
            {
                IEnumerable<KendoMatbaa.Models.IsEmriEski> isEmriEski = db.IsEmriEski;
                isEmriEski = isEmriEski.Where(s => s.ObjectID.ToString().Contains(searchString.ToUpper())
                   || s.isno.ToUpper().Contains(searchString.ToUpper()) || s.adi.ToUpper().Contains(searchString.ToUpper()) || s.birim.ToUpper().Contains(searchString.ToUpper()));
                return View(isEmriEski.ToList());
            }
            
        }



        // GET: IsEmriEski
        //public ActionResult Index()
        //{
        //    return View(db.IsEmriEski.ToList());
        //}

        // GET: IsEmriEski/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriEski isEmriEski = db.IsEmriEski.Find(id);
            if (isEmriEski == null)
            {
                return HttpNotFound();
            }
            return View(isEmriEski);
        }

        // GET: IsEmriEski/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IsEmriEski/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObjectID,isno,tarihsayi,adi,birim,personel,telefon,kabultarihi,teslimtarihi,aciklama1,cinsi,ebadi,adedi,cilt,sayfasayisi,icbaskirenk,kapakbaskirenk,selefon,lak,vernik,icfilm,kapakfilm,ozosolic,estaticic,masteric,CTPic,ozosolkap,estatickap,masterkap,CTPkap,ozic,esic,mtic,CTic,ozkap,eskap,mtkap,CTkap,ickagitcins,ickagitgram,icebad,rexrotaryic,gestetneric,abdickic,multilitic,ryobiic,canonic,rizoic,rexrotarykap,gestetnerkap,abdickkap,multilitkap,ryobikap,canonkap,rizokap,icknet,ickfire,ickbaski,icktabaka,kapakcins,kapakgram,kapakebad,kapaknet,kapakfire,kapakbaski,kapaktabaka,icbaskiebadi,kapakbaskiebadi,icbaskimiktari,kapakbaskimiktari,otokopi,aciklama2,onaybekleyen,onayalinan,isitamamlanan,reddedilen,rednedeni,adetaciklama,IsTamamlandi,OnayGeriAlindi,KismiTamam,YarimKaldi")] IsEmriEski isEmriEski)
        {
            if (ModelState.IsValid)
            {
                db.IsEmriEski.Add(isEmriEski);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(isEmriEski);
        }

        // GET: IsEmriEski/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriEski isEmriEski = db.IsEmriEski.Find(id);
            if (isEmriEski == null)
            {
                return HttpNotFound();
            }
            return View(isEmriEski);
        }

        // POST: IsEmriEski/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObjectID,isno,tarihsayi,adi,birim,personel,telefon,kabultarihi,teslimtarihi,aciklama1,cinsi,ebadi,adedi,cilt,sayfasayisi,icbaskirenk,kapakbaskirenk,selefon,lak,vernik,icfilm,kapakfilm,ozosolic,estaticic,masteric,CTPic,ozosolkap,estatickap,masterkap,CTPkap,ozic,esic,mtic,CTic,ozkap,eskap,mtkap,CTkap,ickagitcins,ickagitgram,icebad,rexrotaryic,gestetneric,abdickic,multilitic,ryobiic,canonic,rizoic,rexrotarykap,gestetnerkap,abdickkap,multilitkap,ryobikap,canonkap,rizokap,icknet,ickfire,ickbaski,icktabaka,kapakcins,kapakgram,kapakebad,kapaknet,kapakfire,kapakbaski,kapaktabaka,icbaskiebadi,kapakbaskiebadi,icbaskimiktari,kapakbaskimiktari,otokopi,aciklama2,onaybekleyen,onayalinan,isitamamlanan,reddedilen,rednedeni,adetaciklama,IsTamamlandi,OnayGeriAlindi,KismiTamam,YarimKaldi")] IsEmriEski isEmriEski)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isEmriEski).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(isEmriEski);
        }

        // GET: IsEmriEski/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriEski isEmriEski = db.IsEmriEski.Find(id);
            if (isEmriEski == null)
            {
                return HttpNotFound();
            }
            return View(isEmriEski);
        }

        // POST: IsEmriEski/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriEski isEmriEski = db.IsEmriEski.Find(id);
            db.IsEmriEski.Remove(isEmriEski);
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
