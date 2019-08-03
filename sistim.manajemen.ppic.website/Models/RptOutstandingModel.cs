using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Models
{
    public class RptOutstandingViewModel : BaseModel
    {
        public List<RptOutstandingModel> ListData { set; get; }
        public RptOutstandingViewModel()
        {
            ListData = new List<RptOutstandingModel>();
        }
    }
    public class RptOutstandingModel
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