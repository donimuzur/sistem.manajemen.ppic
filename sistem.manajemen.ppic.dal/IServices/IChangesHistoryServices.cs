using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IChangesHistoryServices
    {
        List<CHANGES_HISTORY> GetChangesHistory(int modulId, long formId);
    }
}
