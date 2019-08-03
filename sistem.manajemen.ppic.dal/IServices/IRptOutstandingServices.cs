using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IRptOutstandingServices
    {
        List<SP_GetRptOutstanding_Result> GetRpt();
    }
}
