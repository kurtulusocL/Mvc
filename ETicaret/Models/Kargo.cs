using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class Kargo : BaseHome
    {
        public string Adi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string EMail { get; set; }

        public ICollection<Satis> Satises { get; set; }
    }
}