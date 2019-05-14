﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Models
{
    public class TrnPengirimanModel:BaseModel
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string SURAT_JALAN { get; set; }
        public string NO_DO { get; set; }
        public string NO_SPB { get; set; }
        public int NO_TRUCK { get; set; }
        public decimal? JUMLAH { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public string NAMA_BARANG { get; set; }
        public decimal PARTY { get; set; }
        public Nullable<decimal> AKUMULASI { get; set; }
        public Nullable<decimal> SISA { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<core.Enums.StatusDocument> STATUS { get; set; }
        public string CATATAN { get; set; }
        public int NO_SJ { get; set; }
        public string TRUCK { get; set; }
        public string GUDANG { get; set; }
        public Nullable<int> COLLIE { get; set; }
        public int ID_BARANG { get; set; }

        public MstBarangJadiModel MST_BARANG_JADI { get; set; }

    }
    public class TrnPengirimanViewModel : BaseModel
    {
        public List<TrnPengirimanModel> ListData { set; get; }
        public TrnPengirimanViewModel()
        {
            ListData = new List<TrnPengirimanModel>();
        }
    }
}