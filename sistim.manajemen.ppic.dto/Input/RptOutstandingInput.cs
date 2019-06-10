using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dto.Input
{
    public class RptOutstandingInput
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
    }
}
