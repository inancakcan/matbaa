using System;
using System.Collections;
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
using KendoMatbaa.App_Start;
using KendoMatbaa.IKBSServis;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class IsEmriController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /IsEmri/
        public ActionResult Index(string searchString)
        {
            var isemri = db.IsEmri.Include(i => i.Birim);
            if (!String.IsNullOrEmpty(searchString))
            {
                isemri = isemri.Where(s => s.IsEmriId.ToString().Contains(searchString.ToUpper())
                                           || s.IsNo.ToUpper().Contains(searchString.ToUpper()) ||
                                           s.Adi.ToUpper().Contains(searchString.ToUpper()) ||
                                           s.Birim.BirimAdi.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(isemri.ToList());
        }



        public ActionResult TabView(int IsEmriId, int index = 0)
        {
            Query q = new Query();
            ViewBag.IsEmriTeknikBilglerGirilmisMi = q.IsEmriTeknikBilglerGirilmisMi(IsEmriId);
            ViewBag.IsEmriId = IsEmriId;
            ViewBag.index = index;
            return View();
        }





        public ActionResult _DevamEtmekteOlanIsler()
        {
            var oDevamEdenIslerResult = db.spDevamEdenIsler();
            return View(oDevamEdenIslerResult.ToList());
        }


        public ActionResult MyPartialView()
        {
            return PartialView("MyPartialView");
        }



        public ActionResult RefreshAll([DataSourceRequest] DataSourceRequest request)
        {

            using (var matbaa = new matbaaEntities())
            {
                IQueryable<IsEmri> IsEmirleri = matbaa.IsEmri;
                //DataSourceResult result = IsEmirleri.ToDataSourceResult(request);
                //return Json(result, JsonRequestBehavior.AllowGet);


                DataSourceResult result = IsEmirleri.ToDataSourceResult(request, IsEmri => new IsEmri
                {
                    IsEmriId = IsEmri.IsEmriId,
                    IsNo = IsEmri.IsNo,
                    TarihSayi = IsEmri.TarihSayi,
                    Adi = IsEmri.Adi,
                    BirimId = IsEmri.BirimId,
                    Personel = IsEmri.Personel,
                    Telefon = IsEmri.Telefon,
                    KabulTarihi = IsEmri.KabulTarihi,
                    TeslimTarihi = IsEmri.TeslimTarihi,
                    Aciklama = IsEmri.Aciklama,
                    Silindi = IsEmri.Silindi,
                    IKBSBirimUN = IsEmri.IKBSBirimUN
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }


            //assuming db.ProcessAll() will return a list object
            //return Json(db.IsEmri_Re(request));
        }

        // GET: /IsEmri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isemri = db.IsEmri.Find(id);
            if (isemri == null)
            {
                return HttpNotFound();
            }
            return View(isemri);
        }

        // GET: /IsEmri/Create
        //[Authorize(Users = "ukarkin,hkok")]
        public ActionResult Create()
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                ViewBag.OtomatikAlinmisIsNo = OtomatikIsNoAl();
                ViewBag.BirimUN = new SelectList(db.IKBSBirim.OrderBy(x => x.Adi), "BirimUN", "Adi");
                ViewBag.BitmisIsBoyutlariId = new SelectList(db.BitmisIsBoyutlari, "Id", "BitmisIsBoyutuAdi");
                return View();
            }
            else
            {
                return
                    RedirectToAction("Index", "IsEmris")
                        .Warning("Yeni İş Emri bilgisi ekleme yetkisine sahip değilsiniz!");
            }
        }

        // POST: /IsEmri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "IsEmriId,IsNo,TarihSayi,Adi,Personel,Telefon,KabulTarihi,TeslimTarihi,Aciklama,BirimId,Silindi,IlgiliEPosta,IKBSBirimUN,BitmisIsBoyutlariId"
                )] IsEmri isemri)
        {
            if (ModelState.IsValid)
            {
                //ilk eklemeyi yaparken silindiyi false a cek
                isemri.Silindi = false;
                isemri.IsNo = OtomatikIsNoAl();
                db.IsEmri.Add(isemri);
                db.SaveChanges();
                //tam burada  diğer 4 tabloyada gelecek düzenlemeler için insert yapmalıyız
                //spIsEmriInsertEderkenDigerDortTabloyadaInsertEt(IsEmriId) prosedürünü çalıştırmak gerekli
                using (matbaaEntities ent = new matbaaEntities())
                {
                    ent.spIsEmriInsertEderkenDigerDortTabloyadaInsertEt(isemri.IsEmriId);

                }
                return RedirectToAction("Index", "IsEmri").Success("Yeni İş Emri bigisi ekleme işlemi başarılı");
            }

            ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi", isemri.BirimId);
            return RedirectToAction("Index", "IsEmri").Danger("Yeni İş Emri bilgisi ekleme işlemi başarısız");
        }

        // GET: /IsEmri/Edit/5
        public ActionResult Edit(int? id)
        {
            Session["BitmisIsBoyutunaGidecekIsEmriId"] = id.ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isemri = db.IsEmri.Find(id);
            if (isemri == null)
            {
                return HttpNotFound();
            }
            //ViewBag.BirimId = new SelectList(db.Birim.OrderBy(x => x.BirimAdi), "BirimId", "BirimAdi", isemri.BirimId);
            ViewBag.BirimUN = new SelectList(db.IKBSBirim.OrderBy(x => x.Adi), "BirimUN", "Adi", isemri.IKBSBirimUN);
            
            //using (IKBSServis.ikbsSoapClient g = new ikbsSoapClient())
            //{
            //DataSet dsBirimListesi = g.BirimListesi();
            //ViewBag.BirimUN = new SelectList(db.IKBSBirim.OrderBy(x => x.Adi), "BirimUN", "Adi");
            //}
            Query q = new Query();
            if (q.IsEmriOnaylanmisMi(id.Value))
            {
                ViewBag.IsEmriOnaylanmisMi = "E";
            }
            //return View(isemri);
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                return View(isemri);
            }
            else
            {
                //buray uygun bir yonlendirme yap donguyer giriyor

                //  return RedirectToAction("TabView", "IsEmri", new { IsEmriId = id.Value, index = 0 }).Danger("İş Emri bilgisi güncelleme yetkisine sahip değilsiniz!");
                return View(isemri).Warning("Yetkiniz yok!");

            }

        }

        // POST: /IsEmri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "IsEmriId,IsNo,TarihSayi,Adi,Personel,Telefon,KabulTarihi,TeslimTarihi,Aciklama,BirimId,Silindi,IlgiliEPosta,IKBSBirimUN,BitmisIsBoyutlariId"
                )] IsEmri isemri, string BirimUN, IEnumerable<string> OlmayanIsParcalariMultiSelect)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (isemri.KabulTarihi != null)
                {
                    DateTime bak = isemri.KabulTarihi.Value;
                }
                if (ModelState.IsValid)
                {
                    isemri.IKBSBirimUN = Guid.Parse(BirimUN);
                    db.Entry(isemri).State = EntityState.Modified;
                    db.SaveChanges();


                    #region OlmayanIsParcalari

                    db.uspIsEmriYokIsParcalariniSil(isemri.IsEmriId);
                    if (OlmayanIsParcalariMultiSelect != null)
                        foreach (var olmayanIsParcasiId in OlmayanIsParcalariMultiSelect)
                        {
                            db.uspIsEmriYokIsParcasiEkle(isemri.IsEmriId, Convert.ToInt32(olmayanIsParcasiId));
                        }

                    #endregion



                    if (isemri.Silindi.Value)
                    {
                        return
                            RedirectToAction("Index", "IsEmris", new {IsEmriId = isemri.IsEmriId})
                                .Success("İş Emri bigisi güncelleme işlemi başarılı");
                    }
                    else
                    {
                        return
                            RedirectToAction("TabView", "IsEmri", new {IsEmriId = isemri.IsEmriId, index = 0})
                                .Success("İş Emri bigisi güncelleme işlemi başarılı");
                    }

                }
                ViewBag.BirimUN = new SelectList(db.IKBSBirim, "BirimUN", "Adi", isemri.IKBSBirimUN);
                return
                    RedirectToAction("TabView", "IsEmri", new {IsEmriId = isemri.IsEmriId, index = 0})
                        .Danger("İş Emri bilgisi güncelleme işlemi başarısız");
            }
            else
            {
                return
                    RedirectToAction("TabView", "IsEmri", new {IsEmriId = isemri.IsEmriId, index = 0})
                        .Danger("İş Emri bilgisi güncelleme yetkisine sahip değilsiniz!");
            }
        }




        // GET: /IsEmri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isemri = db.IsEmri.Find(id);
            if (isemri == null)
            {
                return HttpNotFound();
            }
            return View(isemri);
        }

        // POST: /IsEmri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                IsEmri isemri = db.IsEmri.Find(id);
                db.IsEmri.Remove(isemri);
                db.SaveChanges();
                db.spIsEmriDeleteEderkenDigerDortTablodandaDeleteEt(isemri.IsEmriId);
                return RedirectToAction("Index", "IsEmris").Success(" İş Emri bigisi silme işlemi başarılı");
            }
            catch
            {
                return RedirectToAction("Index", "IsEmri").Danger(" İş Emri bigisi silme işlemi başarısız");
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


        #region inanc

        public ActionResult IsEmri_Read([DataSourceRequest] DataSourceRequest request, string searchString)
        {
            using (var matbaa = new matbaaEntities())
            {
                IQueryable<IsEmri> IsEmirleri = matbaa.IsEmri;
                //searchString = "belge";
                if (!String.IsNullOrEmpty(searchString))
                {
                    IsEmirleri = IsEmirleri.Where(s => s.IsEmriId.ToString().Contains(searchString.ToUpper())
                                                       || s.IsNo.ToUpper().Contains(searchString.ToUpper()) ||
                                                       s.Adi.ToUpper().Contains(searchString.ToUpper()) ||
                                                       s.Birim.BirimAdi.ToUpper().Contains(searchString.ToUpper()));

                }

                //IsEmirleri = (IQueryable<IsEmri>)IsEmirleri;

                DataSourceResult result = IsEmirleri.ToDataSourceResult(request, IsEmri => new IsEmri
                {
                    IsEmriId = IsEmri.IsEmriId,
                    IsNo = IsEmri.IsNo,
                    TarihSayi = IsEmri.TarihSayi,
                    Adi = IsEmri.Adi,
                    BirimId = IsEmri.BirimId,
                    Personel = IsEmri.Personel,
                    Telefon = IsEmri.Telefon,
                    KabulTarihi = IsEmri.KabulTarihi,
                    TeslimTarihi = IsEmri.TeslimTarihi,
                    Aciklama = IsEmri.Aciklama,
                    Silindi = IsEmri.Silindi,
                    IlgiliEPosta = IsEmri.IlgiliEPosta,
                    IKBSBirimUN = IsEmri.IKBSBirimUN

                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //}
        //public ActionResult IsEmri_Read2([DataSourceRequest]DataSourceRequest request)
        //{
        //    using (var matbaa = new matbaaEntities())
        //    {
        //        IQueryable<IsEmri> IsEmirleri = matbaa.IsEmri;
        //        DataSourceResult result = IsEmirleri.ToDataSourceResult(request);
        //        return Json(result);
        //    }
        //}



        public ActionResult IsEmri_Read2([DataSourceRequest] DataSourceRequest request)
        {
            using (var matbaa = new matbaaEntities())
            {
                IQueryable<IsEmri> IsEmirleri = matbaa.IsEmri;
                DataSourceResult result = IsEmirleri.ToDataSourceResult(request);
                return Json(result);
            }
        }


        public ActionResult IsEmri_Delete([DataSourceRequest] DataSourceRequest request, IsEmri IsEmri)
        {
            IsEmriniSilindiOlarakGuncelle(IsEmri.IsEmriId);
            return Json(new[] {IsEmri}.ToDataSourceResult(request, ModelState));
        }

        public ActionResult IsEmri_Update([DataSourceRequest] DataSourceRequest request, IsEmri IsEmri)
        {
            if (ModelState.IsValid)
            {
                using (var matbaa = new matbaaEntities())
                {
                    // Create a new Product entity and set its properties from the posted ProductViewModel
                    var entity = new IsEmri
                    {
                        IsEmriId = IsEmri.IsEmriId,
                        IsNo = IsEmri.IsNo,
                        TarihSayi = IsEmri.TarihSayi,
                        Adi = IsEmri.Adi,
                        Birim = IsEmri.Birim,
                        Personel = IsEmri.Personel,
                        Telefon = IsEmri.Telefon,
                        KabulTarihi = IsEmri.KabulTarihi,
                        TeslimTarihi = IsEmri.TeslimTarihi,
                        Aciklama = IsEmri.Aciklama,
                        BirimId = IsEmri.BirimId,
                        Silindi = IsEmri.Silindi.Value,
                        IlgiliEPosta = IsEmri.IlgiliEPosta,
                        IKBSBirimUN = IsEmri.IKBSBirimUN
                    };
                    // Attach the entity
                    matbaa.IsEmri.Attach(entity);
                    // Change its state to Modified so Entity Framework can update the existing product instead of creating a new one
                    matbaa.Entry(entity).State = EntityState.Modified;
                    // Or use ObjectStateManager if using a previous version of Entity Framework
                    // northwind.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    // Update the entity in the database
                    matbaa.SaveChanges();
                }
            }
            // Return the updated product. Also return any validation errors.
            return Json(new[] {IsEmri}.ToDataSourceResult(request, ModelState));
        }

        public JsonResult BirimListesiGetir()
        {
            matbaaEntities matbaa = new matbaaEntities();
            return Json(matbaa.Birim, JsonRequestBehavior.AllowGet);
        }

        private void PopulateBirimler()
        {
            matbaaEntities matbaa = new matbaaEntities();
            ViewData["Birimler"] = matbaa.Birim;
        }


        public ActionResult DetailsIsEmriTeknikBilgilerIdli(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriTeknikBilgiler isemriteknikbilgiler =
                db.IsEmriTeknikBilgiler.Find(FindIsEmriTeknikBilgilerByIsEmriId(id.Value));
            if (isemriteknikbilgiler == null)
            {
                return HttpNotFound();
            }
            return View(isemriteknikbilgiler);
        }



        public ActionResult DetailsIsEmriBaskiBilgileriIdli(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriOnayBilgileri isEmriBaskiBilgileri =
                db.IsEmriOnayBilgileri.Find(FindIsEmriBaskiBilgileriByIsEmriId(id.Value));
            if (isEmriBaskiBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriBaskiBilgileri);
        }


        public ActionResult DetailsIsEmriKullanıilacakKagitIdli(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKullanilacakKagit isEmriKullanilacakKagit =
                db.IsEmriKullanilacakKagit.Find(FindIsEmriKullanilacakKagitByIsEmriId(id.Value));
            if (isEmriKullanilacakKagit == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKullanilacakKagit);
        }


        public ActionResult DetailsIsEmriKalipMalzemeGiderleriIdli(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKalipMalzemeGiderleri isEmriKalipMalzemeGiderleri =
                db.IsEmriKalipMalzemeGiderleri.Find(FindIsEmriKalipMalzemeGideriByIsEmriId(id.Value));
            if (isEmriKalipMalzemeGiderleri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKalipMalzemeGiderleri);



        }

        public ActionResult EditKalipMalzemeGiderleri(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //IsEmriKalipMalzemeGiderleri isemrikalipmalzemegiderleri = db.IsEmriKalipMalzemeGiderleri.Find(FindIsEmriKalipMalzemeGideriByIsEmriId(id));
            IsEmriKalipMalzemeGiderleri isemrikalipmalzemegiderleri = db.IsEmriKalipMalzemeGiderleri.Find(id);
            if (isemrikalipmalzemegiderleri == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isemrikalipmalzemegiderleri.IsEmriId);
            //ViewBag.KaliplarId = new SelectList(db.Kaliplar, "KaliplarId", "KalipAdi", isemrikalipmalzemegiderleri.KaliplarId);
            return View(isemrikalipmalzemegiderleri);
        }

        //public ActionResult _IsEmriYokIsParcasi(int IsEmriId)
        //{
        //    var yokIsParcasis = db.IsEmriYokIsParcasi.Find(3);
        //    return PartialView(yokIsParcasis);
        //}
        //public ActionResult _IsEmriYokIsParcasi3(int IsEmriId)
        //{
        //    IEnumerable<uspIsEmriYokISParcasiAdi_Result> sonuclar = db.uspIsEmriYokISParcasiAdi(IsEmriId); 
        //    //List<SelectListItem> items = new List<SelectListItem>();
        //    //Query q = new Query();


        //    //IEnumerable<IsEmriYokIsParcasi> olmayanIsParcalari = q.YokIsParcasiDon(IsEmriId);
        //    List<string> olmayanIsParcalari = new List<string>() {};


        //    //IEnumerable<string> selectedValues= new string[]{"6","7"};
        //    //IEnumerable<string> selectedValues = new List<string>() { "6", "7" };
        //    List<string> selectedValues = new List<string>() {};

        //    foreach (var sonuc in sonuclar)
        //    {
        //        selectedValues.Add(sonuc.Id.ToString());
        //        olmayanIsParcalari.Add(sonuc.YokIsParcasiAdi);
        //    }


        //    SelectList sl = new SelectList(olmayanIsParcalari, "Value", "Text", selectedValues);

        //    ViewData["OlmayanIsParcalari"] = sl;
        //    ViewData["selectedValues"] = selectedValues;
        //    return PartialView();
        //}

        public ActionResult _IsEmriYokIsParcasi(int IsEmriId)
        {
            List<uspIsEmriYokISParcasiAdi_Result> olmayanIsParcalari = db.uspIsEmriYokISParcasiAdi(IsEmriId).ToList();
            List<string> selectedValues = new List<string>() {};
            List<YokIsParcasi> tumIsParcasis = db.YokIsParcasi.ToList();

            foreach (var olmayanIsParcasi in olmayanIsParcalari)
            {
                selectedValues.Add(olmayanIsParcasi.Id.ToString());
            }

            MultiSelectList sl = new MultiSelectList(tumIsParcasis, "Id", "YokIsParcasiAdi");
            ViewData["OlmayanIsParcalari"] = sl;
            ViewData["selectedValues"] = selectedValues;
            return PartialView();
        }

        #endregion inanc

        public ActionResult IsEmrininKOpyasiniOlustur(int isemriid)
        {
            var TazeIsEmriId=db.spIsEmrininKopyasiniOlustur(isemriid).ToList().First().Value;
            return RedirectToAction("TabView", "IsEmri",
                new {IsEmriId = Convert.ToInt32(TazeIsEmriId), index = 0})
                .Success("İş Emrinin kopyası başarılı olarak oluşturuldu");
        }
    }
}