using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Models
{
    public class MstUomModel
    {
        public int ID { get; set; }
        public string SATUAN { get; set; }
        public string DESKRIPSI { get; set; }
        public byte STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
    }
}