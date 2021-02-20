using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class MusteriAdres:BaseHome
    {
        public string Adres { get; set; }
        public string AdiSoyadi { get; set; }

        public int MusteriId { get; set; }
        public virtual Musteri Musteri { get; set; }
    }
}