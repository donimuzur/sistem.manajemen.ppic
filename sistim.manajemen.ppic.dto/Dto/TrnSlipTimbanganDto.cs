using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnSlipTimbanganDto
    {
        public long ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string NO_POLISI { get; set; }
        public string PERUSAHAAN { get; set; }
        public string SUPPLIER { get; set; }
        public Nullable<decimal> BERAT_MASUK { get; set; }
        public Nullable<decimal> BERAT_KELUAR { get; set; }
        public Nullable<decimal> BERAT_BRUTTO { get; set; }
        public Nullable<decimal> BERAT_NETTO { get; set; }
        public Nullable<System.DateTime> JAM_MASUK { get; set; }
        public Nullable<System.DateTime> JAM_KELUAR { get; set; }
        public string BAHAN_BAKU { get; set; }
        public string KETERANGAN { get; set; }
        public Nullable<byte> STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string REMARKS { get; set; }
        public Nullable<long> NO_RECORD { get; set; }
        public string PENGEMUDI { get; set; }
        public string NO_SURAT_JALAN { get; set; }
    }
}
