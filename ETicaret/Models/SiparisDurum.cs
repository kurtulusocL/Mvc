using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class SiparisDurum : BaseHome
    {
        public string Adi { get; set; }
        public string Aciklama { get; set; }

        public ICollection<Satis> Satises { get; set; }
    }
}