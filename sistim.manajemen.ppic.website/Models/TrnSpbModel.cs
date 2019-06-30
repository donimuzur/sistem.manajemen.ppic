using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Models
{
    public class TrnSpbModel:BaseModel
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string NO_SPB { get; set; }
        public string SALES { get; set; }
        public string WILAYAH { get; set; }
        public string NAMA_PRODUK { get; set; }
        public string KOMPOSISI { get; set; }
        public string BENTUK { get; set; }
        public string BENTUK_DESC { get; set; }
        public string KEMASAN { get; set; }
        public string NAMA_KONSUMEN { get; set; }
        public string ALAMAT_KONSUMEN { get; set; }
        public string NO_TELP { get; set; }
        public string CONTACT_PERSON { get; set; }
        public string SEGMEN_PASAR { get; set; }
        public string JENIS_PENJUALAN { get; set; }
        public string JENIS_PENJUALAN_DESC { get; set; }
        public string CARA_PEMBAYARAN { get; set; }
        public decimal? KUANTUM { get; set; }
        public decimal? HARGA_JUAL { get; set; }
        public Nullable<decimal> ONGKOS_KIRIM { get; set; }
        public string PPN { get; set; }
        public decimal? HARGA_LOKO { get; set; }
        public string LOKASI_KIRIM { get; set; }
        public System.DateTime? BATAS_KIRIM { get; set; }
        public string DOKUMEN_PENDUKUNG { get; set; }
        public string DOKUMEN_PENDUKUNG_DESC { get; set; }
        public string DOKUMEN_PENDUKUNG_FILE { get; set; }
        public string DOKUMEN_NOMOR { get; set; }
        public string CATATAN { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string KEMASAN_DESC { get; set; }
        public Nullable<core.Enums.StatusDocument> STATUS { get; set; }
        public Nullable<int> DOKUMEN_PENDUKUNG_TYPE { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }

        public SelectList SegmenPasarList {get;set;}
        public SelectList BentukList {get;set;}
        public SelectList JenisPenjualanList { get; set; }
        public SelectList PPNList { get; set; }
        public SelectList CaraPembayaranList { get; set; }
        public SelectList KemasanList {get;set;}
        public SelectList DokumenList { get; set; }
    }
    public class TrnSpbViewModel:BaseModel
    {
        public List<TrnSpbModel> ListData { get; set; }
        public TrnSpbViewModel()
        {
            ListData = new List<TrnSpbModel>();
        }
    }
}