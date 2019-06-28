using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class SoBarangJadiDetailsDto
    {
        public int ID { get; set; }
        public int ID_SO_BARANG_JADI { get; set; }
        public int ID_BARANG { get; set; }
        public decimal STOCK_SISTEM { get; set; }
        public decimal STOCK_REAL { get; set; }
        public decimal SELISIH { get; set; }
        public string KETERANGAN { get; set; }

        public virtual MstBarangJadiDto MST_BARANG_JADI { get; set; }
        public virtual SoBarangJadiDto SO_BARANG_JADI { get; set; }
    }
}
