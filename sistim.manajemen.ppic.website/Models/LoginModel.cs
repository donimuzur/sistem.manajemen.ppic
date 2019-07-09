using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Models
{
    public class LoginModel : BaseModel
    {
        public string USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public bool STATUS { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public Nullable<core.Enums.Role> ROLE_ID { get; set; }
        public string POSITION { get; set; }
        public DateTime? LAST_ONLINE { get; set; }
    }
}