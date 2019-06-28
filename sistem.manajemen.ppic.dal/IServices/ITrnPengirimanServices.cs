using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnPengirimanServices
    {
        List<TRN_PENGIRIMAN_MASTER> GetAll();
        List<TRN_PENGIRIMAN_DETAILS> GetAllDetails();
        TRN_PENGIRIMAN_MASTER GetTrnPengirimanMasterById(Object Id);
        TRN_PENGIRIMAN_DETAILS GetTrnPengirimanDetailsById(Object Id);
        TRN_PENGIRIMAN_DETAILS GetTrnPengirimanDetailsByPengiriman(int Id);
        void Save(TRN_PENGIRIMAN_MASTER Db, Login Login);
        void Save(TRN_PENGIRIMAN_MASTER Db);
    }
}
