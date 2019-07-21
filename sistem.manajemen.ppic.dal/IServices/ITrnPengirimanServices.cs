using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnPengirimanServices
    {
        List<TRN_PENGIRIMAN> GetAll();
        List<TRN_PENGIRIMAN> GetActiveAll();
        TRN_PENGIRIMAN GetTrnPengirimanMasterById(Object Id);
        TRN_PENGIRIMAN Save(TRN_PENGIRIMAN Db, Login Login);
        void Save(TRN_PENGIRIMAN Db);
        void Delete(int id, string Remarks);
    }
}
