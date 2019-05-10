using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Models
{
    public class TrnHasilProduksiViewModel : BaseModel
    {
        public List<TrnHasilProduksiModel> ListData { set; get; }
        public TrnHasilProduksiViewModel()
        {
            ListData = new List<TrnHasilProduksiModel>();
        }
    }

    public class TrnHasilProduksiModel : BaseModel
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public int ID_BARANG { get; set; }
        public decimal? JUMLAH { get; set; }
        public string CATATAN { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }

        public SelectList BarangList { get; set; }

        public MstBarangJadiModel MST_BARANG_JADI { get; set; }
    }
}