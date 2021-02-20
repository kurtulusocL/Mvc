using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class Marka : BaseHome
    {
        public string Adi { get; set; }
        public string Aciklama { get; set; }

        public int? ResimId { get; set; }
        public virtual Resim Resim { get; set; }

        public ICollection<Urun> Uruns { get; set; }
    }
}