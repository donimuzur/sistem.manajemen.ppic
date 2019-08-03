using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnSuratPerintahProduksiDto
    {
        public int ID { get; set; }
        public string NO_SURAT { get; set; }
        public Nullable<System.DateTime> TANGGAL { get; set; }
        public Nullable<int> TUJUAN_PRODUKSI { get; set; }
        public string NO_SPB { get; set; }
        public string WILAYAH { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public string NAMA_PRODUK { get; set; }
        public string KOMPOSISI { get; set; }
        public string BENTUK { get; set; }
        public string BENTUK_DESC { get; set; }
        public string KEMASAN { get; set; }
        public Nullable<decimal> KUANTUM { get; set; }
        public Nullable<System.DateTime> RENCANA_KIRIM { get; set; }
        public string CATATAN_PPIC { get; set; }
        public Nullable<System.DateTime> TGL_SELESAI { get; set; }
        public string CATATAN_PRODUKSI { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public byte STATUS { get; set; }
        public string REMARKS { get; set; }
        public string NO_DOKUMEN { get; set; }
    }
}
