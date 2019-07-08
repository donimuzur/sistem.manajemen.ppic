using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnMutasiBarangDto
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string DEPT_BAGIAN { get; set; }
        public string NAMA_BARANG { get; set; }
        public string BENTUK { get; set; }
        public string KEMASAN { get; set; }
        public Nullable<int> JENIS_BARANG { get; set; }
        public string SATUAN { get; set; }
        public string LOKASI { get; set; }
        public Nullable<decimal> JUMLAH { get; set; }
        public string KETERANGAN { get; set; }
        public string REMARKS { get; set; }
        public Nullable<byte> STATUS { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
    }
}
