using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class ChangesHistoryServices: IChangesHistoryServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<CHANGES_HISTORY> _changesHistoryRepo;
        public ChangesHistoryServices(IUnitOfWork uow)
        {
            _uow = uow;
            _changesHistoryRepo = _uow.GetGenericRepository<CHANGES_HISTORY>();
        }
        public List<CHANGES_HISTORY> GetChangesHistory(int modulId, long formId)
        {
            var data = _changesHistoryRepo.Get(x => x.MODUL_ID == modulId && x.FORM_ID == formId).ToList();
            return data;
        }
    }
}
