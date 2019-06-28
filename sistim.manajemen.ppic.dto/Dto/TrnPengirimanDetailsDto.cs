using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnPengirimanDetailsDto
    {
        public int ID { get; set; }
        public int ID_TRN_PENGIRIMAN_MASTER { get; set; }
        public int ID_BARANG { get; set; }
        public string URAIAN_BARANG { get; set; }
        public string KEMASAN { get; set; }
        public int ZAK { get; set; }
        public Nullable<decimal> STOCK_AWAL { get; set; }
        public decimal KUANTUM { get; set; }
        public Nullable<decimal> STOCK_AKHIR { get; set; }
        public string KETERANGAN { get; set; }
        public Nullable<byte> STATUS { get; set; }

        public virtual MstBarangJadiDto MST_BARANG_JADI { get; set; }
        public virtual TrnPengirimanMasterDto TRN_PENGIRIMAN_MASTER { get; set; }
    }
}
