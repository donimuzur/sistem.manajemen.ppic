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
    
    public partial class MST_BAHAN_BAKU
    {
        public MST_BAHAN_BAKU()
        {
            this.TRN_PENERIMAAN_BARANG = new HashSet<TRN_PENERIMAAN_BARANG>();
        }
    
        public int ID { get; set; }
        public string NAMA_BARANG { get; set; }
        public decimal STOCK { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<decimal> STOCK_AWAL { get; set; }
        public Nullable<decimal> STOCK_AKHIR { get; set; }
        public string SATUAN { get; set; }
        public Nullable<byte> ISBAHAN_PEMBANTU { get; set; }
    
        public virtual ICollection<TRN_PENERIMAAN_BARANG> TRN_PENERIMAAN_BARANG { get; set; }
    }
}
