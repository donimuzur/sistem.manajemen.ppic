using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Models
{
    public class TrnDoModel:BaseModel
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string NO_SPB { get; set; }
        public string NO_DO { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public decimal? JUMLAH { get; set; }
        public string NAMA_CP { get; set; }
        public string NO_TELP { get; set; }
        public string NAMA_BARANG { get; set; }
        public string KETERANGAN { get; set; }
        public Nullable<decimal> HARGA_LOKO { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<core.Enums.StatusDocument> STATUS { get; set; }
        public string KOMPOSISI { get; set; }
        public string BENTUK { get; set; }
        public string KEMASAN { get; set; }
        public string ALAMAT_KONSUMEN { get; set; }
        public string NO_FAX { get; set; }
    }
    public class TrnDoViewModel : BaseModel
    {
        public List<TrnDoModel> ListData { get; set; }
        public TrnDoViewModel()
        {
            ListData = new List<TrnDoModel>();
        }
    }
}