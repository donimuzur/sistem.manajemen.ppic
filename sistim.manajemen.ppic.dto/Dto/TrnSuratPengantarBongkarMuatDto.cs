using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnSuratPengantarBongkarMuatDto
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string NO_SPB { get; set; }
        public int? NO_DO { get; set; }
        public string TRNSPT_NAMA_PT { get; set; }
        public string TRNSPT_JENIS_KENDARAAN { get; set; }
        public string TRNSPT_NO_POLISI { get; set; }
        public Nullable<System.DateTime> TRNSPT_JAM_DATANG { get; set; }
        public string TRNSPT_NAMA_SOPIR { get; set; }
        public string GUDANG { get; set; }
        public Nullable<int> JENIS_BARANG { get; set; }
        public string NAMA_BARANG { get; set; }
        public Nullable<decimal> KUANTUM { get; set; }
        public string BENTUK { get; set; }
        public string KEMASAN { get; set; }
        public Nullable<System.DateTime> JAM_MUAT { get; set; }
        public Nullable<int> STATUS { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string REMARK { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string NO_SURAT { get; set; }
        public string CATATAN { get; set; }
        public Nullable<int> ZAK { get; set; }
    }
}
