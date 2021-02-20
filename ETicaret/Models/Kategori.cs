using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class Kategori : BaseHome
    {
        public string Adi { get; set; }
        public string Aciklama { get; set; }       

        public ICollection<Urun> Uruns { get; set; }
        public ICollection<OzellikTip> OzellikTips { get; set; }
    }
}