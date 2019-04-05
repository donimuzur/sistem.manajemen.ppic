using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Models
{
    public class MstWilayahModel:BaseModel
    {
        public MstWilayahModel(){
            var ListStatus = new Dictionary<string, bool>();
            ListStatus.Add("Tidak Aktif", false);
            ListStatus.Add("Aktif", true);
            StatusList = new SelectList(ListStatus, "value", "key");
        }
        public int ID { get; set; }
        public string WILAYAH { get; set; }
        public int KODE { get; set; }
        public bool STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }

        public SelectList StatusList { get; set; }
    }
    public class MstWilayahViewModel:BaseModel
    {
        public MstWilayahViewModel()
        {
            ListData = new List<MstWilayahModel>();
        }
        public List<MstWilayahModel> ListData {get;set;}
    }
}