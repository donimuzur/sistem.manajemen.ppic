using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IRptEkspedisiHarianServices
    {
        List<SP_RealisasiHarian_Result> GetRpt(string DateTime);
    }
}
