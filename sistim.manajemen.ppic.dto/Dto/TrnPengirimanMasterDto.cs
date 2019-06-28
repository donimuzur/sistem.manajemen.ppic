﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class TrnPengirimanMasterDto
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
        public string PROVINSI { get; set; }
        public string KABUPATEN { get; set; }
        public string NAMA_PENGIRIM { get; set; }
        public Nullable<decimal> TAMBAHAN_KUANTUM { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public string KETERANGAN { get; set; }
        public byte STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }

        public virtual ICollection<TrnPengirimanDetailsDto> TRN_PENGIRIMAN_DETAILS { get; set; }
    }
}