//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Roller
    {
        public int RollerId { get; set; }
        public string RolAdi { get; set; }
        public System.Guid PersonelUN { get; set; }
        public string RolAciklama { get; set; }
    
        public virtual Personel Personel { get; set; }
    }
}