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
    
    public partial class TRN_PENGIRIMAN
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string SURAT_JALAN { get; set; }
        public string NO_DO { get; set; }
        public string NO_SPB { get; set; }
        public int NO_TRUCK { get; set; }
        public decimal JUMLAH { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public string NAMA_BARANG { get; set; }
        public decimal PARTY { get; set; }
        public decimal AKUMULASI { get; set; }
        public decimal SISA { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<core.Enums.StatusDocument> STATUS { get; set; }
        public string CATATAN { get; set; }
    }
}
