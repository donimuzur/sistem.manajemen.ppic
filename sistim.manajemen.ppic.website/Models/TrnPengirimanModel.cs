﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Models
{
    public class TrnPengirimanModel:BaseModel
    {
        public int ID { get; set; }
        public System.DateTime? TANGGAL { get; set; }
        public string NO_SURAT_JALAN { get; set; }
        public string NO_SPB { get; set; }
        public int? NO_DO { get; set; }
        public int? NO_RIT { get; set; }
        public Nullable<decimal> PARTAI { get; set; }
        public Nullable<decimal> TOTAL_KIRIM { get; set; }
        public Nullable<decimal> SISA_KIRIM { get; set; }
        public string TRNSPT_NAMA_PT { get; set; }
        public string TRNSPT_JENIS_KENDARAAN { get; set; }
        public string TRNSPT_NO_POLISI { get; set; }
        public Nullable<System.DateTime> TRNSPT_BERANGKAT { get; set; }
        public Nullable<System.DateTime> TRNSPT_SAMPAI { get; set; }
        public string TRNSPT_NAMA_SOPIR { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public string ALAMAT_KONSUMEN { get; set; }
        public string PROVINSI { get; set; }
        public string KABUPATEN { get; set; }
        public string NAMA_PENGIRIM { get; set; }
        public Nullable<decimal> TAMBAHAN_KUANTUM { get; set; }
        public string NAMA_BARANG { get; set; }
        public string KEMASAN { get; set; }
        public Nullable<int> ZAK { get; set; }
        public Nullable<decimal> STOCK_AWAL { get; set; }
        public Nullable<decimal> KUANTUM { get; set; }
        public Nullable<decimal> STOCK_AKHIR { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public string KETERANGAN { get; set; }
        public byte STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public int ID_BARANG { get; set; }
        public string REMARK { get; set; }
        public Nullable<decimal> TIMBANGAN { get; set; }


        public SelectList DoList { set; get; }
        public virtual MstBarangJadiModel MST_BARANG_JADI { get; set; }
    }
    public class TrnPengirimanViewModel : BaseModel
    {
        public List<TrnPengirimanModel> ListData { set; get; }
        public SearchView_TrnPengiriman SearchView { get; set; }
        public TrnPengirimanViewModel()
        {
            ListData = new List<TrnPengirimanModel>();
            SearchView = new SearchView_TrnPengiriman();
        }
    }
    public class SearchView_TrnPengiriman
    {
        public DateTime? TANGGAL { set; get; }
        public string NAMA_KONSUMEN { set; get; }
        public string NAMA_BARANG { set; get; }
        public string PROVINSI { get; set; }
        public string KABUPATEN { set; get; }
    }
}