

using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KendoMatbaa.Models
{
    using System;
    using System.Collections.Generic;

    public partial class AltGruplarViewModel
    {
        public AltGruplarViewModel()
        {
            this.AltGruplarSilmeTarihce = new HashSet<AltGruplarSilmeTarihce>();
            this.IsEmriFilmGiderleri = new HashSet<IsEmriFilmGiderleri>();
            this.IsEmriKalipMalzemeGiderleri = new HashSet<IsEmriKalipMalzemeGiderleri>();
            this.IsEmriKullanilanLaminasyonlar = new HashSet<IsEmriKullanilanLaminasyonlar>();
            this.IsEmriOtokopi = new HashSet<IsEmriOtokopi>();
            this.KritikSeviye1 = new HashSet<KritikSeviye>();
            this.KullanilanKagitlar = new HashSet<KullanilanKagitlar>();
            this.StokGiris = new HashSet<StokGiris>();
            this.IsEmriKullanilacakKagit = new HashSet<IsEmriKullanilacakKagit>();
            this.IsEmriKullanilacakKagit1 = new HashSet<IsEmriKullanilacakKagit>();
        }

        public int AltGruplarId { get; set; }
        public Nullable<int> StokGruplariId { get; set; }
        public string AltGrupAdi { get; set; }
        public Nullable<double> KritikSeviye { get; set; }
        public Nullable<bool> Otokopi { get; set; }
        public decimal BirimFiyat { get; set; }
        public bool SarfMi { get; set; }
        public Nullable<int> FizikselBirimId { get; set; }



        public int GuncelStokSayisi
        {
            get
            {
                using (matbaaEntities ent = new matbaaEntities())
                {
                    return Convert.ToInt32(ent.spGuncelStokSayisi(AltGruplarId).First());
                }
            }
        }

        public virtual FizikselBirim FizikselBirim { get; set; }
        public virtual StokGruplari StokGruplari { get; set; }
        public virtual ICollection<AltGruplarSilmeTarihce> AltGruplarSilmeTarihce { get; set; }
        public virtual ICollection<IsEmriFilmGiderleri> IsEmriFilmGiderleri { get; set; }
        public virtual ICollection<IsEmriKalipMalzemeGiderleri> IsEmriKalipMalzemeGiderleri { get; set; }
        public virtual ICollection<IsEmriKullanilanLaminasyonlar> IsEmriKullanilanLaminasyonlar { get; set; }
        public virtual ICollection<IsEmriOtokopi> IsEmriOtokopi { get; set; }
        public virtual ICollection<KritikSeviye> KritikSeviye1 { get; set; }
        public virtual ICollection<KullanilanKagitlar> KullanilanKagitlar { get; set; }
        public virtual ICollection<StokGiris> StokGiris { get; set; }
        public virtual ICollection<IsEmriKullanilacakKagit> IsEmriKullanilacakKagit { get; set; }
        public virtual ICollection<IsEmriKullanilacakKagit> IsEmriKullanilacakKagit1 { get; set; }
    }
}
