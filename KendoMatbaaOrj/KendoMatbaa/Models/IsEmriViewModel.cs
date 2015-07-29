using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoMatbaa.Models
{
    using System;
    using System.Collections.Generic;
    public class IsEmriViewModel
    {

        [Key]
        [ScaffoldColumn(false)]
        public int IsEmriId { get; set; }
        [Required]
        [DisplayName("İş No")]
        public string IsNo { get; set; }
        [DisplayName("Tarih/Sayı")]
        public string TarihSayi { get; set; }
        [DisplayName("Adı")]
        public string Adi { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("Personel")]
        public string Personel { get; set; }
        [DisplayName("Telefon")]



        public string Telefon { get; set; }
        [DisplayName("Kabul Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> KabulTarihi { get; set; }
        [DisplayName("Teslim Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TeslimTarihi { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }

        //[UIHint("GridForeignKey")]
        //public int BirimId { get; set; }

        //[ForeignKey("BirimId")]
        //public Birim fkBirim { get; set; }

        [DisplayName("Silindi")]
        public bool? Silindi { get; set; }
        [DisplayName("İlgilinin E-Posta Adresi")]
        public string IlgiliEPosta { get; set; }
    }
}