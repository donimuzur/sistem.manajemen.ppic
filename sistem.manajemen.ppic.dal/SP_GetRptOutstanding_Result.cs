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
    
    public partial class SP_GetRptOutstanding_Result
    {
        public System.DateTime TANGGAL { get; set; }
        public System.DateTime BATAS_KIRIM { get; set; }
        public string NO_SPB { get; set; }
        public string NO_DO { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public decimal PARTY { get; set; }
        public Nullable<decimal> AKUMULASI { get; set; }
        public Nullable<decimal> SISA { get; set; }
    }
}
