using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;
using KendoMatbaa.YetkiServis;
using Microsoft.AspNet.Identity;
using System.Web.Mvc.Filters;
using System.Net.Mail;



namespace KendoMatbaa.Controllers
{
    public class BaseController : Controller
    {
        public static Guid _projectUN = Guid.Parse("e668ef17-2794-4f08-ab0e-4073c8bb2b1b");
        //Static sorun cikaracak mi kontrol edilecek
        public static Guid _kullaniciUN = Guid.Empty;
        public static string _ticket = string.Empty;

        #region YetkiNet


        //public Guid KullaniciUN
        //{
        //    get
        //    {
        //        _kullaniciUN = KullaniciUNDon();
        //        return _kullaniciUN;
        //    }
        //    set { _kullaniciUN = value; }
        //}

        //public string Ticket
        //{
        //    get
        //    {
        //        _ticket= TicketDon();
        //        return _ticket;
        //    }
        //    set { Session["Ticket"] = value; }
        //}


        //public Guid KullaniciUNDon()
        //{
        //    using (YetkiServisSoapClient client = new YetkiServisSoapClient())
        //    {
        //        Guid locKullaniciUN = Guid.Empty;
        //        KullaniciDataSet dsKullanici = client.TicketGetir(Md5Sifreleme(Ticket));
        //        //ukarkin   :   daadfacd-f637-446f-91ca-cf789caf4ba3
        //        //iakcan    :   67e395d3-ad0e-442f-8f9d-e770910a2e6b
        //        if (Ticket != string.Empty)
        //        {
        //             locKullaniciUN = Guid.Parse(dsKullanici.Tables[0].Rows[0]["KullaniciUN"].ToString());
        //        }
        //        return locKullaniciUN;
        //    }
        //}

        public string TicketDon()
        {
            if (Session["Ticket"] != null)
                return Session["Ticket"].ToString();
            else
            {
                return string.Empty;
            }
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                string InitialURL = Request.Url.AbsoluteUri;
                if (InitialURL.IndexOf("Ticket") == -1)
                {
                    string Url = ConfigurationManager.AppSettings["YetkiGirisSayfasi"] + "?ReturnUrl=" +
                                  Request.Url.GetLeftPart(UriPartial.Path).Replace("http:", "http://").Replace("////", "//") +
                                 "&UN=" +
                                 ConfigurationManager.AppSettings["YetkiProjeUN"];
                    Response.Redirect(Url, true);
                }
                else//Yani ticket i alıp döndü isek
                {
                    //Burada kullanıcıyı otantike etmemiz gereki artık
                    using (YetkiServisSoapClient client = new YetkiServisSoapClient())
                    {
                        KullaniciDataSet dsKullanici = client.TicketGetir(Md5Sifreleme(Request["Ticket"].ToString()));
                        //ukarkin   :   daadfacd-f637-446f-91ca-cf789caf4ba3
                        //iakcan    :   67e395d3-ad0e-442f-8f9d-e770910a2e6b
                        Guid KullaniciUN = Guid.Parse(dsKullanici.Tables[0].Rows[0]["KullaniciUN"].ToString());
                        string Adi = dsKullanici.Tables[0].Rows[0]["Adi"].ToString();
                        string Soyadi = dsKullanici.Tables[0].Rows[0]["Soyadi"].ToString();
                        string KullaniciAdi = dsKullanici.Tables[0].Rows[0]["KullaniciAdi"].ToString();
                        string SicilNo = dsKullanici.Tables[0].Rows[0]["SicilNo"].ToString();
                        string Eposta = dsKullanici.Tables[0].Rows[0]["Eposta"].ToString();
                        string ActivationKey = dsKullanici.Tables[0].Rows[0]["ActivationKey"].ToString();
                        Guid BirimUN = Guid.Parse(dsKullanici.Tables[0].Rows[0]["BirimUN"].ToString());




                        #region CookieOlustur

                        /*
                        MenuDataSet dsMenu = client.KullaniciYetkiliMenulerGetir(KullaniciUN, _projectUN);
                        int RolSayisi=dsMenu.Tables[0].Rows.Count;
                        string[] _roller = new string[RolSayisi];
                        if(RolSayisi>0)
                        {
                            int Sayac=0;
                            
                            foreach (DataRow row in dsMenu.Tables[0].Rows)
	                        {
		                        _roller[Sayac]=row["MenuAdi"].ToString();
                                Sayac++;
	                        }
                        }
                        */

                        YetkiDataSet dsProjeUzerindeKullaniciYetkileri = client.KullaniciYetkilerGetir(KullaniciUN, _projectUN);
                        int YetkiSayisi = dsProjeUzerindeKullaniciYetkileri.Tables[0].Rows.Count;

                        string[] _yetkiler = new string[YetkiSayisi];
                        if (YetkiSayisi > 0)
                        {
                            int Sayac = 0;
                            foreach (DataRow row in dsProjeUzerindeKullaniciYetkileri.Tables[0].Rows)
                            {
                                _yetkiler[Sayac] = row["YetkiAdi"].ToString();
                                Sayac++;
                            }
                        }


                        App_Start.User usr = new User();
                        usr.KullaniciUN = KullaniciUN;
                        usr.Name = Adi;
                        usr.Username = KullaniciAdi;
                        usr.Age = 44;
                        usr.EPosta = Eposta;
                        usr.Roller = _yetkiler;

                        var serializer = new JavaScriptSerializer();

                        string userData = serializer.Serialize(usr);

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                            usr.Username,
                            DateTime.Now,
                            DateTime.Now.AddDays(30),
                            true,
                            userData,
                            FormsAuthentication.FormsCookiePath);

                        // Encrypt the ticket.
                        string encTicket = FormsAuthentication.Encrypt(ticket);

                        // Create the cookie.

                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                        RedirectToAction("Index", "IsEmris");
                        //}
                        #endregion CookieOlustur
                    }

                }
            }
        }
        public MenuDataSet KullaniciMenusuDon()
        {
            if (UserManager.User != null)
            {
                using (YetkiServisSoapClient client = new YetkiServisSoapClient())
                {
                    MenuDataSet dsMenu = client.KullaniciYetkiliMenulerGetir(UserManager.User.KullaniciUN, _projectUN);
                    return dsMenu;
                }
            }
            else
            {
                return null;
            }
        }
        private string Md5SifrelemeString(string sText)
        {
            byte[] md5Sifre = Md5Sifreleme(sText);
            string md5String = "";
            foreach (byte b in md5Sifre)
            {
                md5String += b.ToString();
            }
            return md5String;
        }
        private byte[] Md5Sifreleme(string sText)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] md5Sifre = md5.ComputeHash(encoder.GetBytes(sText));
            return md5Sifre;
        }

        public void IsEmriniSilindiOlarakGuncelle(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                IsEmri SilindiOlarakIslenecekIsEmri = (from c in db.IsEmri
                                                       where c.IsEmriId == IsEmriId
                                                       select c).First();
                SilindiOlarakIslenecekIsEmri.Silindi = true;

                //IsEmri disinda diger 4 destek tablosunda da silindi olarak işaretlenecek

                IsEmriTeknikBilgiler SilindiOlarakIslenecekTeknikBilgi = (from c in db.IsEmriTeknikBilgiler
                                                                          where c.IsEmriId == IsEmriId
                                                                          select c).First();
                SilindiOlarakIslenecekTeknikBilgi.Silindi = true;


                IsEmriOnayBilgileri SilindiOlarakIslenecekBaskiBilgisi = (from c in db.IsEmriOnayBilgileri
                                                                           where c.IsEmriID == IsEmriId
                                                                           select c).First();
                SilindiOlarakIslenecekBaskiBilgisi.Silindi = true;

                IsEmriKullanilacakKagit SilindiOlarakIslenecekKagitBilgisi = (from c in db.IsEmriKullanilacakKagit
                                                                              where c.IsEmriID == IsEmriId
                                                                              select c).First();
                SilindiOlarakIslenecekKagitBilgisi.Silindi = true;

                IsEmriKalipMalzemeGiderleri SilindiOlarakIslenecekKalipMalzemeBilgisi = (from c in db.IsEmriKalipMalzemeGiderleri
                                                                                         where c.IsEmriId == IsEmriId
                                                                                         select c).First();
                SilindiOlarakIslenecekKalipMalzemeBilgisi.Silindi = true;
                db.SaveChanges();
            }
        }
        public string KullaniciBilgisiDon(string IstenilenBilgi)
        {
            string sonuc = string.Empty;
            HttpContext currentContext = System.Web.HttpContext.Current;
            //string userName = currentContext.User.Identity.Name;
            bool isAuthenticated = currentContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string userData = ticket.UserData;
                string[] arrKullaniciBilgisi = userData.Split('|');
                switch (IstenilenBilgi)
                {
                    case "KullaniciUN": sonuc = arrKullaniciBilgisi[0]; break;
                    case "Adi": sonuc = arrKullaniciBilgisi[1]; break;
                    case "Soyadi": sonuc = arrKullaniciBilgisi[2]; break;
                    case "KullaniciAdi": sonuc = arrKullaniciBilgisi[3]; break;
                    case "SicilNo": sonuc = arrKullaniciBilgisi[4]; break;
                    case "Eposta": sonuc = arrKullaniciBilgisi[5]; break;
                    case "ActivationKey": sonuc = arrKullaniciBilgisi[6]; break;
                    case "BirimUN": sonuc = arrKullaniciBilgisi[7]; break;
                }
            }
            return sonuc;
        }

        //public bool KullaniciBelirliBirYetkiyeSahipMi(string YetkiAdi)
        //{
        //    bool Sonuc = false;
        //    using (YetkiServisSoapClient client = new YetkiServisSoapClient())
        //    {
        //        YetkiDataSet dsProjeUzerindeKullaniciYetkileri = client.KullaniciYetkilerGetir(UserManager.User.KullaniciUN, _projectUN);
        //        foreach (DataRow row in dsProjeUzerindeKullaniciYetkileri.Tables[0].Rows)
        //        {
        //            if (row["YetkiAdi"].ToString() == YetkiAdi)
        //            {
        //                Sonuc = true;
        //            }
        //        }
        //    }
        //    return Sonuc;
        //}

        #endregion YetkiNet

        public static User User
        {
            get
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // The user is authenticated. Return the user from the forms auth ticket.
                    return ((MyPrincipal)(System.Web.HttpContext.Current.User)).User;
                }
                else if (System.Web.HttpContext.Current.Items.Contains("User"))
                {
                    // The user is not authenticated, but has successfully logged in.
                    return (User)System.Web.HttpContext.Current.Items["User"];
                }
                else
                {
                    return null;
                }
            }
        }



        public ActionResult IsEmriniOnaylaGerc(int id = 0)
        {
            bool Sonuc = false;
            try
            {
                using (matbaaEntities db = new matbaaEntities())
                {
                    int IsEmriBaskiBilgileriId = FindIsEmriBaskiBilgileriByIsEmriId(id);
                    IsEmriOnayBilgileri tblIsEmriBaskiBilgileri = db.IsEmriOnayBilgileri.Find(IsEmriBaskiBilgileriId);
                    tblIsEmriBaskiBilgileri.Onayalindi = true;
                    db.SaveChanges();
                    Sonuc = true;
                }
            }
            catch (Exception)
            {

            }
            //return Redirect("~/KontrolPaneli/Dashboard").SetStatusMessage("Onaylama başarılı") ;
            return RedirectToAction("Dashboard").SetStatusMessage("Onaylama başarılı");

            //return Redirect("~/IsEmriBaskiBilgileri/Details/" + id.ToString());
            //return View("Details2", "tblIsEmriBaskiBilgileri", new {id = "#=ObjectID#"})

        }


        public bool IsEmriTeknikBilgilerKaydiVarmi(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                bool Sonuc = false;
                int Sayi = (from c in db.IsEmriTeknikBilgiler where c.IsEmriId == IsEmriId select c).ToList().Count();
                if (Sayi > 0)
                    Sonuc = true;
                return Sonuc;
            }
        }

        public int FindIsEmriTeknikBilgilerByIsEmriId(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                var bak = from c in db.IsEmriTeknikBilgiler
                          where c.IsEmriId == IsEmriId
                          select new { c.IsEmriTeknikBilgilerId };
                int Sonuc = bak.First().IsEmriTeknikBilgilerId;
                return Sonuc;
            }
        }

        public int FindIsEmriIdByIsEmriTeknikBilgilerId(int IsEmriTeknikBilgilerId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                var bak = from c in db.IsEmriTeknikBilgiler
                          where c.IsEmriTeknikBilgilerId == IsEmriTeknikBilgilerId
                          select new { c.IsEmriId };
                int Sonuc = bak.First().IsEmriId;
                return Sonuc;
            }
        }


        public int FindIsEmriKullanilanLaminasyonlarIdByIsEmriId(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                int Sonuc = 0;
                var bak = from c in db.IsEmriKullanilanLaminasyonlar
                          where c.IsEmriId == IsEmriId
                          select new { c.KullanilanLaminasyonlarId };
                try
                {
                     Sonuc = bak.FirstOrDefault().KullanilanLaminasyonlarId;
                }
                catch
                {
                     Sonuc = 0;
                }
                
                return Sonuc;
            }
        }


        public int FindIsEmriFilmGiderleriIdByIsEmriId(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                int Sonuc = 0;
                var bak = from c in db.IsEmriFilmGiderleri
                          where c.IsEmriId == IsEmriId
                          select new { c.IsEmriFilmGiderleriId };
                try
                {
                    Sonuc = bak.FirstOrDefault().IsEmriFilmGiderleriId;
                }
                catch
                {
                    Sonuc = 0;
                }

                return Sonuc;
            }
        }


        public string OtomatikIsNoAl()
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                return db.spOtomatikIsNoAl().FirstOrDefault();
            }
        }

        public bool IsEmriBaskiBilgileriKaydiVarmi(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                bool Sonuc = false;
                int Sayi = (from c in db.IsEmriOnayBilgileri where c.IsEmriID == IsEmriId select c).ToList().Count();
                if (Sayi > 0)
                    Sonuc = true;
                return Sonuc;
            }
        }

        public int FindIsEmriBaskiBilgileriByIsEmriId(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {

                var bak = from c in db.IsEmriOnayBilgileri
                          where c.IsEmriID == IsEmriId
                          select new { c.IsEmriBaskiBilgileriId };
                return bak.First().IsEmriBaskiBilgileriId;

            }
        }

        public bool IsEmriKullanilacakKagitBilgileriKaydiVarmi(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                bool Sonuc = false;
                int Sayi = (from c in db.IsEmriKullanilacakKagit where c.IsEmriID == IsEmriId select c).ToList().Count();
                if (Sayi > 0)
                    Sonuc = true;
                return Sonuc;
            }
        }
        public int FindIsEmriKullanilacakKagitByIsEmriId(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                var bak = from c in db.IsEmriKullanilacakKagit
                          where c.IsEmriID == IsEmriId
                          select new { c.IsEmriKullanilacakKagitId };
                return bak.First().IsEmriKullanilacakKagitId;
            }
        }

        public bool IsEmriKalipMalzemeGiderBilgileriKaydiVarmi(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                bool Sonuc = false;
                int Sayi = (from c in db.IsEmriKalipMalzemeGiderleri where c.IsEmriId == IsEmriId select c).ToList().Count();
                if (Sayi > 0)
                    Sonuc = true;
                return Sonuc;
            }
        }
        public int FindIsEmriKalipMalzemeGideriByIsEmriId(int IsEmriId)
        {
            using (matbaaEntities db = new matbaaEntities())
            {
                var bak = from c in db.IsEmriKalipMalzemeGiderleri
                          where c.IsEmriId == IsEmriId
                          select new { c.IsEmriKalipMalzemeGiderleriId };
                return bak.First().IsEmriKalipMalzemeGiderleriId;
            }
        }

        public bool MailGonder(string From,string To,string Subject,string Body)
        {
            bool Sonuc = false;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("10.1.2.5");
                smtpServer.Credentials = new System.Net.NetworkCredential("tse\\noreply", "Nr2020");
                smtpServer.Port = 25; // Gmail works on this port

                //mail.From = new MailAddress("iakcan@tse.org.tr");
                //mail.To.Add("iakcan@tse.org.tr");
                //mail.Subject = "Password recovery";
                //mail.Body = "Recovering the password";

                mail.From = new MailAddress(From);
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.Body = Body;
                
                
                smtpServer.Send(mail);
                Sonuc = true;
            }
            catch (Exception)
            {
                //Sonuc = false;
                //throw;
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("10.1.2.4");
                smtpServer.Credentials = new System.Net.NetworkCredential("tse\\noreply", "Nr2020");
                smtpServer.Port = 25; // Gmail works on this port

                //mail.From = new MailAddress("iakcan@tse.org.tr");
                //mail.To.Add("iakcan@tse.org.tr");
                //mail.Subject = "Password recovery";
                //mail.Body = "Recovering the password";

                mail.From = new MailAddress(From);
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.Body = Body;


                smtpServer.Send(mail);
                Sonuc = true;
            }
            return Sonuc;

        }

        //public spGecenOtuzGunIcindeBitirilenIslerListe_Result spGecenOtuzGunIcindeBitirilenIslerListe()
        //{

        //    using (matbaaEntities db = new matbaaEntities())
        //    {
        //        return db.spGecenOtuzGunIcindeBitirilenIslerListe();
        //    }
        //}

        public static List<Birim> GetAllBirims()
        {
            using (matbaaEntities ent = new matbaaEntities())
            {
                var x = from c in ent.Birim
                        select c;
                return x.ToList();
            }

        }

        public spGuncelStokDurumuOzelRev_Result AltGrupIcinGuncelStokDurumu(int AltGrupId)
        {
            using (matbaaEntities ent = new matbaaEntities())
            {
                return ent.spGuncelStokDurumuOzelRev(AltGrupId).FirstOrDefault();
            }
        }


        //public spAltUretimAsamasiUretimIdden_Result IsEmriveUretimAsamasindanAltUretimAsamalari(int IsEmriId, int UretimAsamasiId)
        //{
            
        //    using (matbaaEntities ent = new matbaaEntities())
        //    {
        //      return ent.spAltUretimAsamasiUretimIdden(IsEmriId, UretimAsamasiId);
        //    }

        //}

        public BaseController()
        {
            ViewBag.Menu = BuildMenu();
        }

        private IList<MenuModel> BuildMenu()
        {

            IList<MenuModel> mmList = new List<MenuModel>() { };

            //BaseController cnt = new BaseController();
            MenuDataSet dsMenu = KullaniciMenusuDon();
            if (dsMenu != null)
            {
                //Once parent
                foreach (DataRow row in dsMenu.Tables[0].Rows)
                {
                    if (row["ParentUN"].ToString() == string.Empty)
                    {
                        //items.Add().Text(row["MenuAdi"].ToString()).Url(row["MenuUrl"].ToString());
                        mmList.Add(new MenuModel() { MenuUN = Guid.Parse(row["MenuUN"].ToString()), MenuAdi = row["MenuAdi"].ToString(), MenuUrl = row["MenuUrl"].ToString(), ParentUN = Guid.Empty, Sira = int.Parse(row["Sira"].ToString()) });
                    }
                }
                //şimdi children
                foreach (DataRow row in dsMenu.Tables[0].Rows)
                {
                    if (row["ParentUN"].ToString() != string.Empty)
                    //if (row["ParentUN"].ToString() != "00000000-0000-0000-0000-000000000000" && row["ParentUN"].ToString() != string.Empty)
                    
                    {
                        //if (row["MenuAdi"].ToString()=="Personel")
                        mmList.Add(new MenuModel() { MenuUN = Guid.Parse(row["MenuUN"].ToString()), MenuAdi = row["MenuAdi"].ToString(), MenuUrl = row["MenuUrl"].ToString(), ParentUN = Guid.Parse(row["ParentUN"].ToString()), Sira = int.Parse(row["Sira"].ToString()) });
                    }
                }
            }
            return mmList;
        }
        public string StokGruplariIddenStokGrubuAdiDon(int StokGruplariId)
        {
            string StokGrupAdi = string.Empty;
            using (matbaaEntities db = new matbaaEntities())
            {
                StokGrupAdi = (from c in db.StokGruplari
                               where c.StokGruplariId == StokGruplariId
                               select new { c.StokGrupAdi }).First().StokGrupAdi;
            }
            return StokGrupAdi;
        }
        public string AltGrupIddenAltGrupAdiDon(int AltGruplarId)
        {
            string AltGrupAdi = string.Empty;
            using (matbaaEntities db = new matbaaEntities())
            {
                AltGrupAdi = (from c in db.AltGruplar
                              where c.AltGruplarId == AltGruplarId
                              select new { c.AltGrupAdi }).First().AltGrupAdi;
            }
            return AltGrupAdi;
        }

        public bool PersonelUretimAsamasiIcinYetkilendirimisMi(int IsEmriId, int AltUretimAsamalariId, string Personel)
        {
            bool Sonuc = false;
            Guid bak = UserManager.User.KullaniciUN;
            using (matbaaEntities db = new matbaaEntities())
            {
                int SatirSayisi = (from c in db.UretimYetkilendirme
                              where c.IsEmriId == IsEmriId && c.AltUretimAsamalariId == AltUretimAsamalariId
                              select new { c.UretimYetkilendirmeId }).Count();
                if (SatirSayisi > 0)
                {
                    Sonuc = true;
                }
            }
            return Sonuc;
        }

        public bool PersonelBelirliBirRoleSahipMi(string RoleAdi)
        {
            bool Sonuc = false;
            using (matbaaEntities db = new matbaaEntities())
            {
                var roller = (from c in db.Roller
                                   where c.PersonelUN == User.KullaniciUN
                                   select new { c.RolAdi });
                foreach (var item in roller)
                {
                    if (item.RolAdi ==RoleAdi)
                    {
                        Sonuc = true;
                    }
                }
            }
            return Sonuc;
        }
        public int  BaskiIcinKapakveIcEbadiDon(int IsEmriId,bool KapakMi)
        {
            int Sonuc = 0;
            using (matbaaEntities db = new matbaaEntities())
            {
                if (KapakMi)
                {
                    try
                    {
                        Sonuc = (from c in db.IsEmriKullanilacakKagit
                                 where c.IsEmriID == IsEmriId
                                 select new { c.KapakBaskiBoyutu }).First().KapakBaskiBoyutu.Value;
                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    try
                    {
                        Sonuc = (from c in db.IsEmriKullanilacakKagit
                                 where c.IsEmriID == IsEmriId
                                 select new { c.IcBaskiBoyutu }).First().IcBaskiBoyutu.Value;
                    }
                    catch
                    {
                    }
                }
            }
            return Sonuc;
        }

    }
}