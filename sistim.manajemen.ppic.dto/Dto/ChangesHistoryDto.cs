using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class ChangesHistoryDto
    {
        public long ID {get;set;}
        public int MODUL_ID {get;set;}
        public long FORM_ID { get; set;}
        public string MODIFIED_BY {get;set;}
        public Nullable<System.DateTime> MODIFIED_DATE {get;set;}
        public string ACTION {get;set;}
    }
}
