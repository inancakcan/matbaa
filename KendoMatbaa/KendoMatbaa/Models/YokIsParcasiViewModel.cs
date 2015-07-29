using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace KendoMatbaa.Models
{
    using System;
    using System.Collections.Generic;
    public class YokIsParcasiViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Olmayan iş parçasını seçmelisiniz")]
        public string YokIsParcasiAdi { get; set; }
    }
}