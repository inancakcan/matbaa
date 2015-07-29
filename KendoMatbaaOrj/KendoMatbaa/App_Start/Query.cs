using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using KendoMatbaa.YetkiServis;

namespace KendoMatbaa.App_Start
{
    public class Query
    {
        private matbaaEntities db=new matbaaEntities();

        public string IlgilininAdiniDon(int IsEmriId)
        {
            string Sonuc = string.Empty;
            var query = from isEmris in db.IsEmri
                        where isEmris.IsEmriId== IsEmriId
                        select isEmris;
            Sonuc = query.First().Personel;
            return Sonuc;
        }
        public string IlgilininEPostaAdresiniDon(int IsEmriId)
        {
            string Sonuc = string.Empty;
            var query = from isEmris in db.IsEmri
                        where isEmris.IsEmriId == IsEmriId
                        select isEmris;
            Sonuc = query.First().IlgiliEPosta;
            return Sonuc;
        }
        public string IsEmriIddenIsNoDon(int IsEmriId)
        {
            string Sonuc = string.Empty;
            var query = from isEmris in db.IsEmri
                        where isEmris.IsEmriId == IsEmriId
                        select isEmris;
            Sonuc = query.First().IsNo;
            return Sonuc;
        }


        public string TaahhutEdilenTeslimTarihiniDon(int IsEmriId)
        {
            string Sonuc = string.Empty;
            var query = from isEmris in db.IsEmri
                        where isEmris.IsEmriId == IsEmriId
                        select isEmris;
            Sonuc = query.First().TeslimTarihi.ToString();
            if (Sonuc != String.Empty)
            {
                return Sonuc;
            }
            else
            {
                return DateTime.Today.ToString(CultureInfo.CurrentCulture);
            }
        }

        public string AltUretimAsamalariIddenAsamaAdiDon(int AltUretimAsamalariId)
        {
            string Sonuc = string.Empty;
            var query = from altUretimAsamalris in db.AltUretimAsamalari
                        where altUretimAsamalris.AltUretimAsamalariId== AltUretimAsamalariId
                        select altUretimAsamalris;
            Sonuc = query.First().AltUretimAsamalariAdi;
            return Sonuc;
        }

        //public string PersonelUNdenPeronelMailiniDon(Guid PersonelUN)
        public string PersonelUNdenPeronelMailiniDon(Guid PersonelUN)
        {
            string Sonuc = string.Empty;
            var query = from personels in db.Personel
                        where personels.PersonelUN == PersonelUN
                        select personels;
            Sonuc = query.First().EPosta;
            return Sonuc;
        }

        public string PersonelUNdenPeronelAdiSoyadiniDon(Guid PersonelUN)
        {
            string Sonuc = string.Empty;
            var query = from personels in db.Personel
                        where personels.PersonelUN == PersonelUN
                        select personels;
            Sonuc = query.First().PersonelAdiSoyadi;
            return Sonuc;
        }
        public bool IsEmriOnaylanmisMi(int IsEmriId)
        {
            bool Sonuc = false;
            var query = from isEmriOnayBilgileris in db.IsEmriOnayBilgileri
                        where isEmriOnayBilgileris.IsEmriID == IsEmriId
                        select isEmriOnayBilgileris;
            if (query.FirstOrDefault() != null)
            {
                if (query.FirstOrDefault().Onayalindi.Value)
                {
                    Sonuc = true;
                }
            }

            return Sonuc;
        }

        public bool IsEmriTeknikBilglerGirilmisMi(int IsEmriId)
        {
            bool Sonuc = false;
            var query = from c in db.IsEmriTeknikBilgiler
                        where c.IsEmriId == IsEmriId
                        select c;
            if (query.Count()>0)
            {
                Sonuc = true;
            }

            return Sonuc;
        }

        public Guid UretimYetkilendirmeIddenOYetkilendirmeninPersonelGuidiniDon(int UretimYetkilendirmeId)
        {
            Guid Sonuc = Guid.Empty;
            var query = from urtYetkilendirme in db.UretimYetkilendirme
                        where urtYetkilendirme.UretimYetkilendirmeId == UretimYetkilendirmeId
                        select urtYetkilendirme;
            if (query.Any())
            {
                Sonuc = query.First().PersonelUN;
            }
            return Sonuc;
        }
        //public bool IseSimdiBasladim(int UretimId)
        //{
        //    bool Basarim = false;
        //    try
        //    {
        //        var uretim = (from c in db.Uretim
        //                      where c.UretimId == UretimId
        //                      select c).First();
        //        uretim.UretimeBaslama = DateTime.Now;
        //        db.SaveChanges();
        //        Basarim = true;
        //    }
        //    catch { }
        //    return Basarim;
        //}

        public Uretim UretimYetkilendirmeBilgisindenUretimBilgisiDon(int UretimYetkilendirmeId)
        {
            var uretimYetkilendirme=(from c in db.UretimYetkilendirme
                              where c.UretimYetkilendirmeId == UretimYetkilendirmeId
                              select c).First();

                    Uretim uretim = (from c in db.Uretim
                              where c.IsEmriId==uretimYetkilendirme.IsEmriId 
                              && c.AltUretimAsamalariId==uretimYetkilendirme.AltUretimAsamalariId 
                              && c.Personel==uretimYetkilendirme.PersonelUN 
                              select c).First();
                    return uretim;
        }


        public int isEmriUploadDosyalariIddenIsEmriIdDon(int IsEmriUploadDosyalariId)
        {
            var isEmriUploadDosyalari = (from c in db.IsEmriUploadDosyalari
                                         where c.IsEmriUploadDosyalariId == IsEmriUploadDosyalariId
                                       select c).First();

            
            return isEmriUploadDosyalari.IsEmriId;
        }
        public string KapakResmininPathiniDon(int IsEmriId)
        {
            var isEmriUploadDosyalari = (from c in db.IsEmriUploadDosyalari
                                         where c.IsEmriId == IsEmriId
                                         select c).FirstOrDefault();

            if (isEmriUploadDosyalari != null)
            {
                return isEmriUploadDosyalari.DosyaAdi;
            }
            else
            {
                return "NoImage.png";
            }
        }
        public int IsNodanIsEmriIdDon(string IsNo)
        {
            int Sonuc = 0;
            var query = from isEmris in db.IsEmri
                        where isEmris.IsNo== IsNo
                        select isEmris;
            Sonuc = query.First().IsEmriId;
            return Sonuc;
        }


        //public string IsEmriIddenIsNoDon(int IsEmriId)
        //{
        //    string Sonuc = String.Empty;
        //    var query = from isEmris in db.IsEmri
        //                where isEmris.IsEmriId == IsEmriId
        //                select isEmris;
        //    Sonuc = query.First().IsNo;
        //    return Sonuc;
        //}


        public int UretiIddenIsEmriIdDon(int UretimId)
        {
            int Sonuc = 0;
            var query = (from uretim in db.Uretim
                         where uretim.UretimId == UretimId
                         select uretim);
            Sonuc = query.FirstOrDefault().IsEmriId;           
            return Sonuc;
        }
        public bool IsiBitirdim (int UretimId)
        {
            
                bool BasariliMi = false;
                try
                {
                    var query = (from uretim in db.Uretim
                                 where uretim.UretimId == UretimId
                                 select uretim).First();
                    query.UretimiSonlandirma = DateTime.Now;
                    db.SaveChanges();
                    BasariliMi = true;
                }
                catch
                {
                    BasariliMi = false;
                }
                return BasariliMi;
        }
        public bool IseBaslanmisMi(int UretimYetkilendirmeId)
        {
            bool Sonuc=false;
            int UretimId = UretimYetkilendirmeBilgisindenUretimBilgisiDon(UretimYetkilendirmeId).UretimId;
             var query = (from uretim in db.Uretim
                         where uretim.UretimId == UretimId
                         select uretim);
            if(query.FirstOrDefault().UretimeBaslama.ToString()!=string.Empty)
            {
                Sonuc=true;
            }
            return Sonuc;
        }

        public IsEmriKullanilacakKagit IsEmriIdedenKullanilacakKagitBaskiBoyutlariDon(int IsEmriId)
        {
         try
                {
                    IsEmriKullanilacakKagit isEmriKullanilacakKagit = (from kullanilacakKagit in db.IsEmriKullanilacakKagit
                                 where kullanilacakKagit.IsEmriID== IsEmriId
                                 select kullanilacakKagit).FirstOrDefault();

                    return isEmriKullanilacakKagit;

                }
                catch
                {
                    return null;
                }
        }

        public bool IsEmriYokIsParcasiTablosunaEkle(int IsEmriId, string YokIsParcasiAdi)
        {
            bool basarim = false;
            using (matbaaEntities ent = new matbaaEntities())
            {
                IsEmriYokIsParcasi isEmriYokIsParcasi =new IsEmriYokIsParcasi();
                isEmriYokIsParcasi.IsEmriId = IsEmriId;
                switch (YokIsParcasiAdi)
                {
                    case "Kapak":
                        isEmriYokIsParcasi.YokIsParcasiId = 1;
                        break;
                        case "Film":
                        isEmriYokIsParcasi.YokIsParcasiId = 2;
                        break;
                         case "Laminasyon":
                        isEmriYokIsParcasi.YokIsParcasiId = 3;
                        break;
                }
                try
                {
                    ent.IsEmriYokIsParcasi.Add(isEmriYokIsParcasi);
                    ent.SaveChanges();basarim = true;

                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }
            return basarim;
        }

        public IEnumerable<IsEmriYokIsParcasi> YokIsParcasiDon(int IsEmriId)
        {
            IEnumerable<IsEmriYokIsParcasi> query = (from isEmriYokIsParcasi in db.IsEmriYokIsParcasi where isEmriYokIsParcasi.IsEmriId == IsEmriId select isEmriYokIsParcasi).ToList();
            return query ;
        }


        public bool IsEmriOlmayanIsParcasiniIceriyorMu(int yokIsParcasiId,int isEmriId)
        {
            bool sonuc = false;
            IEnumerable<IsEmriYokIsParcasi> query = (from isEmriYokIsParcasi in db.IsEmriYokIsParcasi where isEmriYokIsParcasi.IsEmriId == isEmriId && isEmriYokIsParcasi.YokIsParcasiId==yokIsParcasiId select isEmriYokIsParcasi).ToList();
            if (query.Count() > 0)
            {
                sonuc = true;
            }
            return sonuc;
        }


        public int GuncelStokSayisi(int AltGruplarId)
        {
            using (matbaaEntities ent = new matbaaEntities())
            {
                return Convert.ToInt32(ent.spGuncelStokSayisi(AltGruplarId).First());
            }
        }



    }
}