using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Models
{
    public class MstBahanBakuModel : BaseModel
    {
        public MstBahanBakuModel()
        {
            var ListStatus = new Dictionary<string, bool>();
            ListStatus.Add("Tidak Aktif", false);
            ListStatus.Add("Aktif", true);
            StatusList = new SelectList(ListStatus, "value", "key");
        }
        public int ID { get; set; }
        public string NAMA_BARANG { get; set; }
        public decimal? STOCK { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<decimal> STOCK_AWAL { get; set; }
        public Nullable<decimal> STOCK_AKHIR { get; set; }
        public string SATUAN { get; set; }
        public Nullable<byte> ISBAHAN_PEMBANTU { get; set; }

        public SelectList StatusList { get; set; }
        public SelectList UomList { set; get; }
    }
    public class MstBahanBakuViewModel : BaseModel
    {
        public List<MstBahanBakuModel> ListData { get; set; }
        public MstBahanBakuViewModel()
        {
            ListData = new List<MstBahanBakuModel>();
        }
    }
}