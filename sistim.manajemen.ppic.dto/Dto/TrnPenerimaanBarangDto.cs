using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnPenerimaanBarangDto
    {
        public int ID { get; set; }
        public string NO_SURAT { get; set; }
        public string SUPPLIER { get; set; }
        public string NOMOR_PO { get; set; }
        public Nullable<int> NOMOR_TTB { get; set; }
        public Nullable<System.DateTime> TANGGAL { get; set; }
        public string NAMA_BARANG { get; set; }
        public string SATUAN { get; set; }
        public decimal KUANTUM { get; set; }
        public decimal JUMLAH { get; set; }
        public string CATATAN { get; set; }
        public string REMARKS { get; set; }
        public Nullable<byte> STATUS { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public int ID_BAHAN_BAKU { get; set; }
        public Nullable<decimal> STOCK_AWAL { get; set; }
        public Nullable<decimal> STOCK_AKHIR { get; set; }
        public virtual MstBahanBakuDto MST_BAHAN_BAKU { get; set; }
    }
}
