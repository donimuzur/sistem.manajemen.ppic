using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.IServices;
using sistem.manajemen.ppic.dal.Services;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll
{
    public class PageBLL : IPageBLL
    {
        private IGenericRepository<PAGE> _pageRepository;
        private IChangesHistoryServices _changesHistoryServices;
        private IUnitOfWork _uow;
        public PageBLL(IUnitOfWork uow)
        {
            _uow = uow;
            _pageRepository = _uow.GetGenericRepository<PAGE>();
            _changesHistoryServices = new ChangesHistoryServices(_uow);
        }
        public List<PAGE> GetPages()
        {
            return _pageRepository.Get().ToList();
        }

        public PAGE GetPageByID(int id)
        {
            var Page = new PAGE();
            return _pageRepository.GetByID(id);
        }
      
        public List<PAGE> GetModulePages()
        {
            return _pageRepository.Get(q => q.PARENT_PAGE_ID != null && q.PARENT_PAGE_ID != 1, null, "").ToList();
        }

        public List<PAGE> GetParentPages()
        {
            var arrParent = new List<int?> { 1, 3 };
            return _pageRepository.Get(p => arrParent.Any(x => x == p.PARENT_PAGE_ID)).ToList();
        }
        public List<ChangesHistoryDto> GetChangesHistory(int modulId, long formId)
        {
            var data = _changesHistoryServices.GetChangesHistory(modulId,formId);
            return Mapper.Map<List<ChangesHistoryDto>>(data);
        }
    }
}
