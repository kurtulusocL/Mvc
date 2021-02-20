using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class Satis : BaseHome
    {
        public DateTime SatisTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public bool SepetteMi { get; set; }
        public string KargoTakipNo { get; set; }

        public int MusteriId { get; set; }
        public int KargoId { get; set; }
        public int SiparisDurumId { get; set; }

        public virtual Musteri Musteri { get; set; }
        public virtual Kargo Kargo { get; set; }
        public virtual SiparisDurum SiparisDurum { get; set; }

        public ICollection<SatisDetay> SatisDetays { get; set; }

        public Satis()
        {
            SepetteMi = true;
        }
    }
}