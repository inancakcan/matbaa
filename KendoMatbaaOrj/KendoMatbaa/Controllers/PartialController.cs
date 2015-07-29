using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Infrastructure.Implementation;
using KendoMatbaa.Models;
using Microsoft.Ajax.Utilities;

namespace KendoMatbaa.Controllers
{
    public class PartialController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();
        //
        // GET: /Partial/
        public PartialViewResult BaskiOncesiHazirlikNotunuDon(int IsEmriId)
        {
            //string UretimNotu = (from oUretimNOtlari in db.UretimNotlari
            //    where oUretimNOtlari.IsEmriId == IsEmriId
            //    select new {oUretimNOtlari.BaskiOncesiHazirlikNotu}).First().BaskiOncesiHazirlikNotu;

            string UretimNotu = db.spIsEmriIddenBaskiOncesiHazirlikNotunuDon(IsEmriId).FirstOrDefault();
            ViewBag.UretimNotu = UretimNotu;
            return PartialView();
        }

        public PartialViewResult _IcBaskiyiYapanlariDon(int IsEmriId)
        {
            IEnumerable<spIcBaskiyiYapanlariDon_Result> oIcBaskiyiYapanlar = db.spIcBaskiyiYapanlariDon(IsEmriId).ToList();
            return PartialView(oIcBaskiyiYapanlar);
        }

        public PartialViewResult _KapakBaskisiniYapanlariDon(int IsEmriId)
        {
            IEnumerable<spKapakBaskisiniYapanlariDon_Result> oKapakBaskisiniYapanlar = db.spKapakBaskisiniYapanlariDon(IsEmriId).ToList();
            return PartialView(oKapakBaskisiniYapanlar);
        }

        public PartialViewResult _TabakaKesiminiYapanlariDon(int IsEmriId)
        {
            IEnumerable<spTabakaKesimleriniYapanlariDon_Result> oTabakaKesimleriniYapanlar = db.spTabakaKesimleriniYapanlariDon(IsEmriId).ToList();
            return PartialView(oTabakaKesimleriniYapanlar);
        }

        public PartialViewResult _HerhangiBirUretimNotunuDon(int AsamaId, int IsEmriId)
        {
            string Not = string.Empty;
                if (AsamaId == 1)
                {
                    if ((from c in db.UretimNotlari
                         where c.IsEmriId == IsEmriId
                         select new { c.BaskiOncesiHazirlikNotu }).Count() > 0)
                    {
                        Not = (from c in db.UretimNotlari
                               where c.IsEmriId == IsEmriId
                               select new { c.BaskiOncesiHazirlikNotu }).First().BaskiOncesiHazirlikNotu;
                    }
                    else
                    {
                        Not = "Girilmemiş";
                    }
                }

                if (AsamaId == 2)
                {
                    if ((from c in db.UretimNotlari
                         where c.IsEmriId == IsEmriId
                         select new { c.BaskiNotu }).Count() > 0)
                    {
                        Not = (from c in db.UretimNotlari
                               where c.IsEmriId == IsEmriId
                               select new { c.BaskiNotu }).First().BaskiNotu;
                    }
                    else
                    {
                        Not = "Girilmemiş";
                    }
                }

                if (AsamaId == 3)
                {
                    if ((from c in db.UretimNotlari
                         where c.IsEmriId == IsEmriId
                         select new { c.MucellithaneNotu }).Count() > 0)
                    {
                        Not = (from c in db.UretimNotlari
                               where c.IsEmriId == IsEmriId
                               select new { c.MucellithaneNotu }).First().MucellithaneNotu;
                    }
                    else
                    {
                        Not = "Girilmemiş";
                    }
                }




            ViewBag.Not = Not;
            return PartialView();
        }

        public PartialViewResult _MucellithaneProgramNoPaketBilgisiDon(int IsEmriId,int Param)
        {
            string Sonuc = string.Empty;
            int bak = 0;
            if (Param == 1)
            {

                bak = (from c in db.MucellithaneProgramNoPaketBilgileri
                    where c.IsEmriId == IsEmriId
                    select new {c.ProgramNo}).Count();
                if (bak > 0)
                {
                    Sonuc = (from c in db.MucellithaneProgramNoPaketBilgileri
                        where c.IsEmriId == IsEmriId
                        select new {c.ProgramNo}).First().ProgramNo.ToString();
                }
                else
                {
                    Sonuc = "-";
                }
            }

            if (Param == 2)
            {
                bak = (from c in db.MucellithaneProgramNoPaketBilgileri
                       where c.IsEmriId == IsEmriId
                       select new { c.IsKesimiProgramNo }).Count();
                if (bak > 0)
                {
                    Sonuc = (from c in db.MucellithaneProgramNoPaketBilgileri
                             where c.IsEmriId == IsEmriId
                             select new { c.IsKesimiProgramNo }).First().IsKesimiProgramNo.ToString();
                }
                else
                {
                    Sonuc = "-";
                }
               
            }

            if (Param == 3)
            {

                bak = (from c in db.MucellithaneProgramNoPaketBilgileri
                       where c.IsEmriId == IsEmriId
                       select new { c.PaketBilgileri }).Count();
                if (bak > 0)
                {
                    var firstOrDefault = (from c in db.MucellithaneProgramNoPaketBilgileri
                        where c.IsEmriId == IsEmriId
                        select new { c.PaketBilgileri }).FirstOrDefault();
                    if (firstOrDefault != null)
                        Sonuc = firstOrDefault.PaketBilgileri;
                }
                else
                {
                    Sonuc = "-";
                }
            }
            ViewBag.Sonuc = Sonuc;
            return PartialView();
        }


        public PartialViewResult _MucellithaneBaskiSonrasiAsama(int IsEmriId)
        {
            var idlist = new int[] { 12, 13, 14, 15, 16, 17, 18 };

            IEnumerable<vUretimTam> oMucellithaneBaskiSonrasiAsamaSet = from x in db.vUretimTam
                         where idlist.Contains(x.AltUretimAsamalariId) && x.IsEmriId==IsEmriId
                         select x;
            return PartialView(oMucellithaneBaskiSonrasiAsamaSet);
        }


        public PartialViewResult _TeslimatRaporu(int IsEmriId)
        {
            //IEnumerable<TeslimatRaporu> oTeslimatRaporu = from x in db.TeslimatRaporu
            //                                              where x.IsEmriId == IsEmriId
            //                                              select x;
            //return PartialView(oTeslimatRaporu);

            IEnumerable<spTeslimatRaporu_Result> oTeslimatRaporu = db.spTeslimatRaporu(IsEmriId).ToList();
            return PartialView(oTeslimatRaporu);
        }


	}
}