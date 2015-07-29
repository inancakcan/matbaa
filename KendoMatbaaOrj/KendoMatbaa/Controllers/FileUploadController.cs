using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using KendoMatbaa.App_Start;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class FileUploadController : BaseController
    {
        //
        // GET: /FileUpload/
        public ActionResult _Async(int IsEmriId)
        {
            ViewBag.IsEmriId = IsEmriId;
            return View();
        }
        public ActionResult Save(IEnumerable<HttpPostedFileBase> files,int IsEmriId)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = IsEmriId.ToString() + "_" + Path.GetFileName(file.FileName);
                    //orj_var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/imgUpload"), fileName);

                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);
                    IsEmriDosyasiniVeriTabaninaYaz(IsEmriId, (string)fileName, "-", (string)physicalPath);
                }
                //Query q = new Query();
                //int IsEmriIdd = q.isEmriUploadDosyalariIddenIsEmriIdDon(isEmriUploadDosyalariId);
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriId, index = 7 }).Success("Dosya ekleme işlemi başarılı"); ;
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriId, index = 7 }).Warning("Dosya yüklenmedi"); 
            }

            // Return an empty string to signify success
            //return Content("");
        }

        public ActionResult DosyayiSil(string filePath)
        {
            string fullPath = Request.MapPath(filePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            return RedirectToAction("Index", "IsEmri").Success("Dosya silme işlemi başarılı");
        }

        public bool IsEmriDosyasiniVeriTabaninaYaz(int isEmriId,string dosyaAdi,string dosyaAciklamasi,string dosyaKonumu)
        {
            bool Sonuc = false;
            using (matbaaEntities ent = new matbaaEntities())
            {
                try
                {
                    ent.spIsEmriDosyasiYaz(isEmriId, dosyaAdi, dosyaAciklamasi, dosyaKonumu);
                    Sonuc = true;
                }
                catch (Exception)
                {
                    Sonuc = false;
                }
                
            }
            return Sonuc;
        }

        public string[] DosyayiAdiniBilesenlereAyir(string DosyaAdi)
        {
            //string[] Sonuc={};
            string[] arrSonuc = DosyaAdi.Split('.');
            return arrSonuc;
        }

        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/imgUpload"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
	}
}