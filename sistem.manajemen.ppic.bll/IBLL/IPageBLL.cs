using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IPageBLL
    {
        List<PAGE> GetPages();
        PAGE GetPageByID(int id);
        List<PAGE> GetModulePages();
        List<PAGE> GetParentPages();
        List<ChangesHistoryDto> GetChangesHistory(int modulId, long formId);
    }
}
