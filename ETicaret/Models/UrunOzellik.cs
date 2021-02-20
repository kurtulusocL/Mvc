using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class UrunOzellik : BaseHome
    {
        public int? UrunId { get; set; }
        public int OzellikTipId { get; set; }
        public int OzellikDegerId { get; set; }

        public virtual Urun Urun { get; set; }
        public virtual OzellikTip OzellikTip { get; set; }
        public virtual OzellikDeger OzellikDeger { get; set; }
    }
}