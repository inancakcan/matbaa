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
    
    public partial class StokGruplariSilmeTarihce
    {
        public int StokGruplariSilmeTarihceId { get; set; }
        public Nullable<int> StokGruplariId { get; set; }
        public string KimNeZaman { get; set; }
    
        public virtual StokGruplari StokGruplari { get; set; }
    }
}
