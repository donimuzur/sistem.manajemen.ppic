using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class SoBarangJadiDto
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public byte STATUS { get; set; }
        public string DOC_NUMBER { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }

        public virtual ICollection<SoBarangJadiDetailsDto> SO_BARANG_JADI_DETAILS { get; set; }
    }
}
