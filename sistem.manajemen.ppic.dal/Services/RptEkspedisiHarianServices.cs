using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class RptEkspedisiHarianServices : IRptEkspedisiHarianServices
    {
        private IUnitOfWork _uow;

        public RptEkspedisiHarianServices(IUnitOfWork Uow)
        {
            _uow = Uow;
        }
        public List<SP_RealisasiHarian_Result> GetRpt(string DateTime)
        {
            var Data = new List<SP_RealisasiHarian_Result>();
            using (var EMSDD_CONTEXT = new PPICEntities())
            {
                EMSDD_CONTEXT.Database.CommandTimeout = 1000;
                var result = EMSDD_CONTEXT.SP_RealisasiHarian(DateTime);
                return result.ToList();
            }
        }
    }
}
