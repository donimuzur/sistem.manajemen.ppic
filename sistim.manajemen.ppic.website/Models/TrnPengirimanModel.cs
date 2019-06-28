using System;
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
    public class TrnPengirimanMasterModel
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL { get; set; }
        public string NO_SURAT_JALAN { get; set; }
        public string NO_SPB { get; set; }
        public int NO_DO { get; set; }
        public int NO_RIT { get; set; }
        public Nullable<decimal> PARTAI { get; set; }
        public Nullable<decimal> TOTAL_KIRIM { get; set; }
        public Nullable<decimal> SISA_KIRIM { get; set; }
        public string TRNSPT_NAMA_PT { get; set; }
        public string TRNSPT_JENIS_KENDARAAN { get; set; }
        public string TRNSPT_NO_POLISI { get; set; }
        public Nullable<System.DateTime> TRNSPT_BERANGKAT { get; set; }
        public Nullable<System.DateTime> TRNSPT_SAMPAI { get; set; }
        public string TRNSPT_NAMA_SOPIR { get; set; }
        public string ALAMAT_PENGIRIM { get; set; }
        public string NAMA_PENGIRIM { get; set; }
        public Nullable<decimal> TAMBAHAN_KUANTUM { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public string KETERANGAN { get; set; }
        public byte STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }

        public virtual List<TrnPengirimanDetailsModel> TRN_PENGIRIMAN_DETAILS { get; set; }


        public string NO_DO_RIT { get; set; }
    }
    public class TrnPengirimanDetailsModel
    {
        public int ID { get; set; }
        public int ID_TRN_PENGIRIMAN_MASTER { get; set; }
        public string URAIAN_BARANG { get; set; }
        public string KEMASAN { get; set; }
        public int ZAK { get; set; }
        public decimal KUANTUM { get; set; }
        public string KETERANGAN { get; set; }
        public Nullable<byte> STATUS { get; set; }
        public int ID_BARANG { get; set; }

        public virtual TrnPengirimanMasterModel TRN_PENGIRIMAN_MASTER { get; set; }
        public virtual MstBarangJadiModel MST_BARANG_JADI { get; set; }
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