using sistem.manajemen.ppic.dto;
using sistem.manajemen.ppic.dto.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IRptOutstandingBLL
    {
        List<RptOutstandingDto> GetRpt();
    }
}
