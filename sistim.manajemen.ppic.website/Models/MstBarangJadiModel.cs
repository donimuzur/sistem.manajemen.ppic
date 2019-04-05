using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static sistem.manajemen.ppic.core.Enums;

namespace sistem.manajemen.ppic.website.Models
{
    public class MstBarangJadiModel : BaseModel
    {
        public MstBarangJadiModel()
        {
            var ListStatus = new Dictionary<string, bool>();
            ListStatus.Add("Tidak Aktif", false);
            ListStatus.Add("Aktif", true);
            StatusList = new SelectList(ListStatus, "value", "key");

            var ListKemasan= new List<string>();
            var Kemasannames = Enum.GetNames(typeof(Kemasan));
            foreach (var name in Kemasannames)
            {
                var value = (Kemasan)Enum.Parse(typeof(Kemasan), name);
                var Description = Enums.GetEnumDescription((Kemasan)value);
                ListKemasan.Add(Description);
            }
            KemasanList = new SelectList(ListKemasan);

            var ListBentuk = new List<string>();
            var Bentuknames = Enum.GetNames(typeof(Bentuk));
            foreach (var name in Bentuknames)
            {
                var value = (Bentuk)Enum.Parse(typeof(Bentuk), name);
                var Description = Enums.GetEnumDescription((Bentuk)value);
                ListBentuk.Add(Description);
            }
            BentukList = new SelectList(ListBentuk);

        }
        public int ID { get; set; }
        public string NAMA_BARANG { get; set; }
        public string KOMPOSISI { get; set; }
        public string BENTUK { get; set; }
        public string BENTUK_LAIN { get; set; }
        public string KEMASAN { get; set; }
        public decimal STOCK { get; set; }
        public bool STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }

        public SelectList StatusList { get; set; }
        public SelectList KemasanList { get; set; }
        public SelectList BentukList { get; set; }
    }
    public class MstBarangJadiViewModel : BaseModel
    {
        public List<MstBarangJadiModel> ListData {get;set;}
        public MstBarangJadiViewModel ()
        {
            ListData = new List<MstBarangJadiModel>();
        }
    } 
}