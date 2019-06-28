using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class MstBarangJadiDto
    {
        public int ID { get; set; }
        public string NAMA_BARANG { get; set; }
        public string KOMPOSISI { get; set; }
        public string BENTUK { get; set; }
        public string BENTUK_LAIN { get; set; }
        public string KEMASAN { get; set; }
        public decimal STOCK { get; set; }
        public bool STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<decimal> STOCK_AWAL { get; set; }
        public Nullable<decimal> STOCK_AKHIR { get; set; }
        
    }
}
