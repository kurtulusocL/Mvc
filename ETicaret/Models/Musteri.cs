using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class Musteri : BaseHome
    {
        public string AdiSoyadi { get; set; }
        public string EMail { get; set; }
        public string TelefonNo { get; set; }

        public ICollection<Satis> Satises { get; set; }
        public ICollection<MusteriAdres> MusteriAdreses { get; set; }
    }
}