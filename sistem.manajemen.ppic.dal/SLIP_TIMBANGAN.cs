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
    
    public partial class SLIP_TIMBANGAN
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