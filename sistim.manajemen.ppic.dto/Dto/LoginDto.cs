using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto
{
    public class LoginDto
    {
        public string USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public bool STATUS { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public Nullable<int> ROLE_ID { get; set; }
        public string POSITION { get; set; }
        public string LAST_ONLINE { get; set; }
    }
}
