using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class OzellikTip : BaseHome
    {       
        public string Adi { get; set; }
        public string Aciklama { get; set; }

        public int KategoriId { get; set; }
        public virtual Kategori Kategori { get; set; }

        public ICollection<OzellikDeger> OzellikDegers { get; set; }
        public ICollection<UrunOzellik> UrunOzelliks { get; set; }
        //subcategory
    }
}