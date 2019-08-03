using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.website.Models
{
    public class MstKemasanModel : BaseModel
    {
        public int ID { get; set; }
        public string KEMASAN { get; set; }
        public Nullable<byte> STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<decimal> CONVERTION { get; set; }
    }
    public class MstKemasanViewModel : BaseModel
    {
        public List<MstKemasanModel> ListData { set; get; }
        public MstKemasanViewModel()
        {
            ListData = new List<MstKemasanModel>();
        }
    }
}
