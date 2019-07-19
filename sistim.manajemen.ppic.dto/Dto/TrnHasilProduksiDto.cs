using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnHasilProduksiDto
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public int ID_BARANG { get; set; }
        public decimal JUMLAH { get; set; }
        public string CATATAN { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<decimal> STOCK_AWAL { get; set; }
        public Nullable<decimal> STOCK_AKHIR { get; set; }
        public Nullable<System.DateTime> TANGGAL_PRODUKSI { get; set; }
        public string REMARKS { get; set; }
        public Nullable<byte> STATUS { get; set; }
        public string NAMA_BARANG { get; set; }
        public string KEMASAN { get; set; }
        public string BENTUK { get; set; }
        public Nullable<decimal> ZAK { get; set; }
        public Nullable<decimal> KUANTUM { get; set; }
        public string LOKASI_PRODUKSI { get; set; }
        public Nullable<int> SHIFT { get; set; }
        public string NO_SURAT { get; set; }

        public MstBarangJadiDto MST_BARANG_JADI { get; set; }
    }
}
