using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Configuration;
namespace KendoMatbaa.Controllers
{
    public class StokGruplariController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /StokGruplari/
        public ActionResult Index()
        {
            return View(db.StokGruplari.ToList().Where(a=>a.Silindi!=true));
        }
        public ActionResult StokGrubununAltGruplari(int id)
        {
            using (matbaaEntities ent = new matbaaEntities())
            {
                ViewBag.StokGrupAdi = StokGruplariIddenStokGrubuAdiDon(id);

                var x = from c in ent.AltGruplar where c.StokGruplariId==id
                        select c;
                return View(x.ToList());
            }

            return View();
        }

        // GET: /StokGruplari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokGruplari stokgruplari = db.StokGruplari.Find(id);
            if (stokgruplari == null)
            {
                return HttpNotFound();
            }
            return View(stokgruplari);
        }

        // GET: /StokGruplari/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StokGruplari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StokGruplariId,StokGrupAdi,Silindi")] StokGruplari stokgruplari)
        {
            if (ModelState.IsValid)
            {
                db.StokGruplari.Add(stokgruplari);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stokgruplari);
        }

        // GET: /StokGruplari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokGruplari stokgruplari = db.StokGruplari.Find(id);
            if (stokgruplari == null)
            {
                return HttpNotFound();
            }
            return View(stokgruplari);
        }

        // POST: /StokGruplari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StokGruplariId,StokGrupAdi,Silindi")] StokGruplari stokgruplari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stokgruplari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stokgruplari);
        }

        // GET: /StokGruplari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokGruplari stokgruplari = db.StokGruplari.Find(id);
            if (stokgruplari == null)
            {
                return HttpNotFound();
            }
            return View(stokgruplari);
        }

        // POST: /StokGruplari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
            StokGruplari stokgruplari = db.StokGruplari.Find(id);
            db.StokGruplari.Remove(stokgruplari);
            db.SaveChanges();
             */ 
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

        public ActionResult AltGtupStokGirisleri(int id)
        {
            using (matbaaEntities ent = new matbaaEntities())
            {
                ViewBag.AltGrupAdi = AltGrupIddenAltGrupAdiDon(id);
                IEnumerable<spStokGiren_Result> giren = ent.spStokGiren(id, DateTime.Parse("01.01.1900"), DateTime.Now);
                return View(giren.ToList());
            }
        }
        public ActionResult AltGtupStokCikislari(int id)
        {
            using (matbaaEntities ent = new matbaaEntities())
            {
                ViewBag.AltGrupAdi = AltGrupIddenAltGrupAdiDon(id);
                IEnumerable<spStokCikan_Result> cikan = ent.spStokCikan(id, DateTime.Parse("01.01.1900"), DateTime.Now);
                return View(cikan.ToList());
            }
        }
        public ActionResult DetailsReport()
        {
            LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();

            string deviceInfo = "<DeviceInfo>" +
            " <OutputFormat>PDF</OutputFormat>" +
            " <PageWidth>8.5in</PageWidth>" +
            " <PageHeight>11in</PageHeight>" +
            " <MarginTop>0.5in</MarginTop>" +
            " <MarginLeft>1in</MarginLeft>" +
            " <MarginRight>1in</MarginRight>" +
            " <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            lr.ReportPath = Server.MapPath("~/Rapor/Birim.rdlc");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["matbaaConnectionString"].ConnectionString);
            conn.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select * from Birim", conn);

            DataSet ds = new DataSet();
            adap.Fill(ds);
            conn.Close();

            lr.DataSources.Clear();
            lr.DataSources.Add(
            new ReportDataSource("DataSetBirim", ds.Tables[0]));
            Warning[] warnings; string mimeType; string encoding; string[] streamids; string fne;
            byte[] bytes = lr.Render("PDF", deviceInfo, out mimeType, out encoding, out fne, out streamids, out warnings);
            return File(bytes, mimeType);
        }

    }
}
