using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class OzellikDeger : BaseHome
    {
        public string Adi { get; set; }
        public string Aciklama { get; set; }

        public int? OzellikTipId { get; set; }
        public virtual OzellikTip OzellikTip { get; set; }

        public ICollection<UrunOzellik> UrunOzelliks { get; set; }
    }
}