using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoMatbaa.Models
{
    public class ZamanaGoreGelenIsYogunluguModel : GelenIsYogunlugu
    {
        public ZamanaGoreGelenIsYogunluguModel()
        {
        }

        public ZamanaGoreGelenIsYogunluguModel(int geri4, int geri3, int geri2, int geri1)
        {
            C_4 = geri4;
            C_3 = geri3;
            C_2 = geri2;
            C_1 = geri1;
        }
    }
}