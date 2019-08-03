using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class RptOutstandingServices : IRptOutstandingServices
    {
        private IUnitOfWork _uow;

        public RptOutstandingServices(IUnitOfWork Uow)
        {
            _uow = Uow;
        }
        public List<SP_GetRptOutstanding_Result> GetRpt()
        {
            var Data = new List<SP_GetRptOutstanding_Result>();
            using (var EMSDD_CONTEXT = new PPICEntities())
            {
                EMSDD_CONTEXT.Database.CommandTimeout = 1000;
                var result = EMSDD_CONTEXT.SP_GetRptOutstanding();
                return result.ToList();
            }
        }
    }
}
