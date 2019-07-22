using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class RptEkspedisiHarianDto
    {
        public string NO_DO { get; set; }
        public string NO_SPB { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public string TUJUAN_KIRIM { get; set; }
        public string ALAMAT_KONSUMEN { get; set; }
        public string NAMA_BARANG { get; set; }
        public string KEMASAN { get; set; }
        public decimal JUMLAH { get; set; }
        public Nullable<int> ZAK { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public Nullable<decimal> TOTAL_TIMBANGAN { get; set; }
        public Nullable<int> TOTAL_TRUK { get; set; }
    }
}
