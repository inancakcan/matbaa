﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KendoMatbaa.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class matbaaEntities : DbContext
    {
        public matbaaEntities()
            : base("name=matbaaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AktiviteAdlari> AktiviteAdlari { get; set; }
        public virtual DbSet<AktiviteSahipleri> AktiviteSahipleri { get; set; }
        public virtual DbSet<AltGruplarSilmeTarihce> AltGruplarSilmeTarihce { get; set; }
        public virtual DbSet<AltUretimAsamalari> AltUretimAsamalari { get; set; }
        public virtual DbSet<AsamaAdlari> AsamaAdlari { get; set; }
        public virtual DbSet<AsamaAyrinti> AsamaAyrinti { get; set; }
        public virtual DbSet<AsamaTarihce> AsamaTarihce { get; set; }
        public virtual DbSet<Birim> Birim { get; set; }
        public virtual DbSet<CiltTipleri> CiltTipleri { get; set; }
        public virtual DbSet<IsAsamasi> IsAsamasi { get; set; }
        public virtual DbSet<IsDurumAdlari> IsDurumAdlari { get; set; }
        public virtual DbSet<IsEmriOtokopi> IsEmriOtokopi { get; set; }
        public virtual DbSet<KagitGramajlari> KagitGramajlari { get; set; }
        public virtual DbSet<KullanilanKagitlar> KullanilanKagitlar { get; set; }
        public virtual DbSet<Maliyet> Maliyet { get; set; }
        public virtual DbSet<OtokopiRenk> OtokopiRenk { get; set; }
        public virtual DbSet<StokGruplari> StokGruplari { get; set; }
        public virtual DbSet<StokGruplariSilmeTarihce> StokGruplariSilmeTarihce { get; set; }
        public virtual DbSet<StokHareketSilmeTarihce> StokHareketSilmeTarihce { get; set; }
        public virtual DbSet<TabakaKesimleri> TabakaKesimleri { get; set; }
        public virtual DbSet<TicariKagitBoyutlari> TicariKagitBoyutlari { get; set; }
        public virtual DbSet<Uretim> Uretim { get; set; }
        public virtual DbSet<UretimAsamalari> UretimAsamalari { get; set; }
        public virtual DbSet<UretimNotlari> UretimNotlari { get; set; }
        public virtual DbSet<UretimYetkilendirme> UretimYetkilendirme { get; set; }
        public virtual DbSet<KritikSeviye> KritikSeviye { get; set; }
        public virtual DbSet<MucellithaneProgramNoPaketBilgileri> MucellithaneProgramNoPaketBilgileri { get; set; }
        public virtual DbSet<TeslimatRaporu> TeslimatRaporu { get; set; }
        public virtual DbSet<IsEmriBaskiMakineleri> IsEmriBaskiMakineleri { get; set; }
        public virtual DbSet<IsEmriOnayBilgileri> IsEmriOnayBilgileri { get; set; }
        public virtual DbSet<IsEmriKullanilanLaminasyonlar> IsEmriKullanilanLaminasyonlar { get; set; }
        public virtual DbSet<IsEmriFilmGiderleri> IsEmriFilmGiderleri { get; set; }
        public virtual DbSet<IsEmriKalipMalzemeGiderleri> IsEmriKalipMalzemeGiderleri { get; set; }
        public virtual DbSet<Roller> Roller { get; set; }
        public virtual DbSet<IsEmriUploadDosyalari> IsEmriUploadDosyalari { get; set; }
        public virtual DbSet<vUretimTam> vUretimTam { get; set; }
        public virtual DbSet<IsCinsi> IsCinsi { get; set; }
        public virtual DbSet<IsEmriEski> IsEmriEski { get; set; }
        public virtual DbSet<IKBSBirim> IKBSBirim { get; set; }
        public virtual DbSet<IsEmriKullanilacakKagit> IsEmriKullanilacakKagit { get; set; }
        public virtual DbSet<IsEmriYokIsParcasi> IsEmriYokIsParcasi { get; set; }
        public virtual DbSet<YokIsParcasi> YokIsParcasi { get; set; }
        public virtual DbSet<BitmisIsBoyutlari> BitmisIsBoyutlari { get; set; }
        public virtual DbSet<IsEmri> IsEmri { get; set; }
        public virtual DbSet<IsEmriTeknikBilgiler> IsEmriTeknikBilgiler { get; set; }
        public virtual DbSet<BaskiEbatlari> BaskiEbatlari { get; set; }
        public virtual DbSet<BaskiMakineleri> BaskiMakineleri { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<vKagitTuketimi> vKagitTuketimi { get; set; }
        public virtual DbSet<FizikselBirim> FizikselBirim { get; set; }
        public virtual DbSet<StokHareket> StokHareket { get; set; }
        public virtual DbSet<AltGruplar> AltGruplar { get; set; }
        public virtual DbSet<StokGiris> StokGiris { get; set; }
    
        public virtual ObjectResult<spGuncelStokDurumuOzelRev_Result> spGuncelStokDurumuOzelRev(Nullable<int> altGrupID)
        {
            var altGrupIDParameter = altGrupID.HasValue ?
                new ObjectParameter("AltGrupID", altGrupID) :
                new ObjectParameter("AltGrupID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGuncelStokDurumuOzelRev_Result>("spGuncelStokDurumuOzelRev", altGrupIDParameter);
        }
    
        public virtual ObjectResult<string> spOtomatikIsNoAl()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spOtomatikIsNoAl");
        }
    
        public virtual ObjectResult<spGelenIsYogunlugu_Result> spGelenIsYogunlugu(string zamanBirimi)
        {
            var zamanBirimiParameter = zamanBirimi != null ?
                new ObjectParameter("ZamanBirimi", zamanBirimi) :
                new ObjectParameter("ZamanBirimi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGelenIsYogunlugu_Result>("spGelenIsYogunlugu", zamanBirimiParameter);
        }
    
        public virtual ObjectResult<spKritikSeviyeKontrol_Result> spKritikSeviyeKontrol()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spKritikSeviyeKontrol_Result>("spKritikSeviyeKontrol");
        }
    
        public virtual ObjectResult<Nullable<int>> spGecenOtuzGunIcindeBitirilenIsler()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spGecenOtuzGunIcindeBitirilenIsler");
        }
    
        public virtual int spIsEmrineOnayVer(Nullable<int> isEmriId, string onaylayan)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            var onaylayanParameter = onaylayan != null ?
                new ObjectParameter("Onaylayan", onaylayan) :
                new ObjectParameter("Onaylayan", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spIsEmrineOnayVer", isEmriIdParameter, onaylayanParameter);
        }
    
        public virtual ObjectResult<spUretimTakipFormu_Result> spUretimTakipFormu(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spUretimTakipFormu_Result>("spUretimTakipFormu", isEmriIdParameter);
        }
    
        public virtual ObjectResult<string> spIsEmriIddenBaskiNotunuDon(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spIsEmriIddenBaskiNotunuDon", isEmriIdParameter);
        }
    
        public virtual ObjectResult<string> spIsEmriIddenBaskiOncesiHazirlikNotunuDon(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spIsEmriIddenBaskiOncesiHazirlikNotunuDon", isEmriIdParameter);
        }
    
        public virtual ObjectResult<string> spIsEmriIddenMucellithaneNotunuDon(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spIsEmriIddenMucellithaneNotunuDon", isEmriIdParameter);
        }
    
        public virtual ObjectResult<spIcBaskiyiYapanlariDon_Result> spIcBaskiyiYapanlariDon(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIcBaskiyiYapanlariDon_Result>("spIcBaskiyiYapanlariDon", isEmriIdParameter);
        }
    
        public virtual ObjectResult<spKapakBaskisiniYapanlariDon_Result> spKapakBaskisiniYapanlariDon(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spKapakBaskisiniYapanlariDon_Result>("spKapakBaskisiniYapanlariDon", isEmriIdParameter);
        }
    
        public virtual ObjectResult<spTabakaKesimleriniYapanlariDon_Result> spTabakaKesimleriniYapanlariDon(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spTabakaKesimleriniYapanlariDon_Result>("spTabakaKesimleriniYapanlariDon", isEmriIdParameter);
        }
    
        public virtual ObjectResult<spTeslimatRaporu_Result> spTeslimatRaporu(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spTeslimatRaporu_Result>("spTeslimatRaporu", isEmriIdParameter);
        }
    
        public virtual int spIsEmriInsertEderkenDigerDortTabloyadaInsertEt(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spIsEmriInsertEderkenDigerDortTabloyadaInsertEt", isEmriIdParameter);
        }
    
        public virtual int spIsEmriDeleteEderkenDigerDortTablodandaDeleteEt(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spIsEmriDeleteEderkenDigerDortTablodandaDeleteEt", isEmriIdParameter);
        }
    
        public virtual ObjectResult<spGecenOtuzGunIcindeBitirilenIslerListe_Result> spGecenOtuzGunIcindeBitirilenIslerListe()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGecenOtuzGunIcindeBitirilenIslerListe_Result>("spGecenOtuzGunIcindeBitirilenIslerListe");
        }
    
        public virtual ObjectResult<spIsemriOnayBilgileriIleBirlikte_Result> spIsemriOnayBilgileriIleBirlikte()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIsemriOnayBilgileriIleBirlikte_Result>("spIsemriOnayBilgileriIleBirlikte");
        }
    
        public virtual ObjectResult<spDevamEdenIsler_Result> spDevamEdenIsler()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spDevamEdenIsler_Result>("spDevamEdenIsler");
        }
    
        public virtual ObjectResult<spAylaraGoreKagitHarcamasi_Result> spAylaraGoreKagitHarcamasi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spAylaraGoreKagitHarcamasi_Result>("spAylaraGoreKagitHarcamasi");
        }
    
        public virtual int spIsEmriDosyasiYaz(Nullable<int> isEmriId, string dosyaAdi, string dosyaAciklamasi, string dosyaKonumu)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            var dosyaAdiParameter = dosyaAdi != null ?
                new ObjectParameter("DosyaAdi", dosyaAdi) :
                new ObjectParameter("DosyaAdi", typeof(string));
    
            var dosyaAciklamasiParameter = dosyaAciklamasi != null ?
                new ObjectParameter("DosyaAciklamasi", dosyaAciklamasi) :
                new ObjectParameter("DosyaAciklamasi", typeof(string));
    
            var dosyaKonumuParameter = dosyaKonumu != null ?
                new ObjectParameter("DosyaKonumu", dosyaKonumu) :
                new ObjectParameter("DosyaKonumu", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spIsEmriDosyasiYaz", isEmriIdParameter, dosyaAdiParameter, dosyaAciklamasiParameter, dosyaKonumuParameter);
        }
    
        public virtual ObjectResult<spIsEmriDosyalariniDon_Result> spIsEmriDosyalariniDon(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIsEmriDosyalariniDon_Result>("spIsEmriDosyalariniDon", isEmriIdParameter);
        }
    
        public virtual int spIsEmriDosyasiniSil(Nullable<int> isEmriUploadDosyalariId)
        {
            var isEmriUploadDosyalariIdParameter = isEmriUploadDosyalariId.HasValue ?
                new ObjectParameter("isEmriUploadDosyalariId", isEmriUploadDosyalariId) :
                new ObjectParameter("isEmriUploadDosyalariId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spIsEmriDosyasiniSil", isEmriUploadDosyalariIdParameter);
        }
    
        public virtual ObjectResult<spAltUretimAsamasiAltUretimAsamasiIdden_Result> spAltUretimAsamasiAltUretimAsamasiIdden(Nullable<int> isEmriId, Nullable<int> altUretimAsamalariId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            var altUretimAsamalariIdParameter = altUretimAsamalariId.HasValue ?
                new ObjectParameter("AltUretimAsamalariId", altUretimAsamalariId) :
                new ObjectParameter("AltUretimAsamalariId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spAltUretimAsamasiAltUretimAsamasiIdden_Result>("spAltUretimAsamasiAltUretimAsamasiIdden", isEmriIdParameter, altUretimAsamalariIdParameter);
        }
    
        public virtual int spUpdateIsCinsiSilindi(Nullable<int> isCinsiId, Nullable<bool> silindi)
        {
            var isCinsiIdParameter = isCinsiId.HasValue ?
                new ObjectParameter("IsCinsiId", isCinsiId) :
                new ObjectParameter("IsCinsiId", typeof(int));
    
            var silindiParameter = silindi.HasValue ?
                new ObjectParameter("Silindi", silindi) :
                new ObjectParameter("Silindi", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateIsCinsiSilindi", isCinsiIdParameter, silindiParameter);
        }
    
        public virtual ObjectResult<spStokCikan_Result> spStokCikan(Nullable<int> altGruplarId, Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var altGruplarIdParameter = altGruplarId.HasValue ?
                new ObjectParameter("AltGruplarId", altGruplarId) :
                new ObjectParameter("AltGruplarId", typeof(int));
    
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spStokCikan_Result>("spStokCikan", altGruplarIdParameter, basTarParameter, sonTarParameter);
        }
    
        public virtual ObjectResult<spStokGiren_Result> spStokGiren(Nullable<int> altGruplarId, Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var altGruplarIdParameter = altGruplarId.HasValue ?
                new ObjectParameter("AltGruplarId", altGruplarId) :
                new ObjectParameter("AltGruplarId", typeof(int));
    
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spStokGiren_Result>("spStokGiren", altGruplarIdParameter, basTarParameter, sonTarParameter);
        }
    
        public virtual ObjectResult<spIsEmriBilgiGirisiTamamlanmismi_Result> spIsEmriBilgiGirisiTamamlanmismi(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIsEmriBilgiGirisiTamamlanmismi_Result>("spIsEmriBilgiGirisiTamamlanmismi", isEmriIdParameter);
        }
    
        public virtual ObjectResult<spTeslimatOrani_Result> spTeslimatOrani(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spTeslimatOrani_Result>("spTeslimatOrani", isEmriIdParameter);
        }
    
        public virtual ObjectResult<uspUretimYetkilendirmeArama_Result> uspUretimYetkilendirmeArama(Nullable<System.Guid> personelUN, string isNo, Nullable<bool> uretimDevamEdiyormu)
        {
            var personelUNParameter = personelUN.HasValue ?
                new ObjectParameter("PersonelUN", personelUN) :
                new ObjectParameter("PersonelUN", typeof(System.Guid));
    
            var isNoParameter = isNo != null ?
                new ObjectParameter("IsNo", isNo) :
                new ObjectParameter("IsNo", typeof(string));
    
            var uretimDevamEdiyormuParameter = uretimDevamEdiyormu.HasValue ?
                new ObjectParameter("UretimDevamEdiyormu", uretimDevamEdiyormu) :
                new ObjectParameter("UretimDevamEdiyormu", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspUretimYetkilendirmeArama_Result>("uspUretimYetkilendirmeArama", personelUNParameter, isNoParameter, uretimDevamEdiyormuParameter);
        }
    
        public virtual ObjectResult<spIsEmriRaporu_Result> spIsEmriRaporu(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIsEmriRaporu_Result>("spIsEmriRaporu", isEmriIdParameter);
        }
    
        public virtual ObjectResult<uspIsEmriYokISParcasiAdi_Result> uspIsEmriYokISParcasiAdi(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspIsEmriYokISParcasiAdi_Result>("uspIsEmriYokISParcasiAdi", isEmriIdParameter);
        }
    
        public virtual int uspIsEmriYokIsParcalariniSil(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspIsEmriYokIsParcalariniSil", isEmriIdParameter);
        }
    
        public virtual int uspIsEmriYokIsParcasiEkle(Nullable<int> isEmriId, Nullable<int> yokIsParcasiId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            var yokIsParcasiIdParameter = yokIsParcasiId.HasValue ?
                new ObjectParameter("YokIsParcasiId", yokIsParcasiId) :
                new ObjectParameter("YokIsParcasiId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspIsEmriYokIsParcasiEkle", isEmriIdParameter, yokIsParcasiIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> spIsEmrininKopyasiniOlustur(Nullable<int> isEmriId)
        {
            var isEmriIdParameter = isEmriId.HasValue ?
                new ObjectParameter("IsEmriId", isEmriId) :
                new ObjectParameter("IsEmriId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spIsEmrininKopyasiniOlustur", isEmriIdParameter);
        }
    
        public virtual ObjectResult<spKagitTuketimi_Result> spKagitTuketimi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spKagitTuketimi_Result>("spKagitTuketimi");
        }
    
        public virtual ObjectResult<Nullable<double>> spGuncelStokSayisi(Nullable<int> altGruplarId)
        {
            var altGruplarIdParameter = altGruplarId.HasValue ?
                new ObjectParameter("AltGruplarId", altGruplarId) :
                new ObjectParameter("AltGruplarId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("spGuncelStokSayisi", altGruplarIdParameter);
        }
    
        public virtual ObjectResult<spIcFireTuketimiAnaliz_Result> spIcFireTuketimiAnaliz(Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIcFireTuketimiAnaliz_Result>("spIcFireTuketimiAnaliz", basTarParameter, sonTarParameter);
        }
    
        public virtual ObjectResult<spIcKagitTuketimiAnaliz_Result> spIcKagitTuketimiAnaliz(Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIcKagitTuketimiAnaliz_Result>("spIcKagitTuketimiAnaliz", basTarParameter, sonTarParameter);
        }
    
        public virtual ObjectResult<spKapakFireTuketimiAnaliz_Result> spKapakFireTuketimiAnaliz(Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spKapakFireTuketimiAnaliz_Result>("spKapakFireTuketimiAnaliz", basTarParameter, sonTarParameter);
        }
    
        public virtual ObjectResult<spKapakKagitTuketimiAnaliz_Result> spKapakKagitTuketimiAnaliz(Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spKapakKagitTuketimiAnaliz_Result>("spKapakKagitTuketimiAnaliz", basTarParameter, sonTarParameter);
        }
    
        public virtual ObjectResult<spIcBaskiMakineleriAnaliz_Result> spIcBaskiMakineleriAnaliz(Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spIcBaskiMakineleriAnaliz_Result>("spIcBaskiMakineleriAnaliz", basTarParameter, sonTarParameter);
        }
    
        public virtual ObjectResult<spKapakBaskiMakineleriAnaliz_Result> spKapakBaskiMakineleriAnaliz(Nullable<System.DateTime> basTar, Nullable<System.DateTime> sonTar)
        {
            var basTarParameter = basTar.HasValue ?
                new ObjectParameter("BasTar", basTar) :
                new ObjectParameter("BasTar", typeof(System.DateTime));
    
            var sonTarParameter = sonTar.HasValue ?
                new ObjectParameter("SonTar", sonTar) :
                new ObjectParameter("SonTar", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spKapakBaskiMakineleriAnaliz_Result>("spKapakBaskiMakineleriAnaliz", basTarParameter, sonTarParameter);
        }
    }
}
