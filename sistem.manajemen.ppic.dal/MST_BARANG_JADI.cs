//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sistem.manajemen.ppic.dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class MST_BARANG_JADI
    {
        public MST_BARANG_JADI()
        {
            this.TRN_PENGIRIMAN = new HashSet<TRN_PENGIRIMAN>();
            this.TRN_HASIL_PRODUKSI = new HashSet<TRN_HASIL_PRODUKSI>();
        }
    
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
    
        public virtual ICollection<TRN_PENGIRIMAN> TRN_PENGIRIMAN { get; set; }
        public virtual ICollection<TRN_HASIL_PRODUKSI> TRN_HASIL_PRODUKSI { get; set; }
    }
}
