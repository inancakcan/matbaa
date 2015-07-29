using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using ModelBinderAttribute = System.Web.Http.ModelBinding.ModelBinderAttribute;

namespace KendoMatbaa.Controllers
{
    public class AltGrupIcinGuncelStokDurumuController : BaseController
    {
         public ActionResult GuncelStokDurumu()
        {
            var result = AltGrupIcinGuncelStokDurumu(278);
            return View(result);
        }

	}
}