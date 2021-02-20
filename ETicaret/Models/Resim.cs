using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class Resim : BaseHome
    {
        public string BuyukYol { get; set; }
        public string OrtaYol { get; set; }
        public string KucukYol { get; set; }
        public bool Varsayılan { get; set; }
        public int SiraNo { get; set; }

        public int? UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        public ICollection<Marka> Markas { get; set; }
    }
}