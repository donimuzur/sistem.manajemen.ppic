using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Models
{
    public class RptOutstandingListModel : BaseModel
    {
        public RptOutstandingListModel()
        {
            SearchViewExport = new CfmIdleSearchViewExport();
            SearchView = new RptOutstandingModelSearchView();
            ListRptOutstanding = new List<RptOutstandingModel>();
        }

        public List<RptOutstandingModel> ListRptOutstanding { get; set; }
        public RptOutstandingModelSearchView SearchView { get; set; }
        public CfmIdleSearchViewExport SearchViewExport { get; set; }
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
    public class RptOutstandingModelSearchView
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
       
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
    }
    public class CfmIdleSearchViewExport
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
    }
}