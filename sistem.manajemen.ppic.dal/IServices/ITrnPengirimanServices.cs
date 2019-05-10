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
        TRN_PENGIRIMAN GetById(object Id);
        void Save(TRN_PENGIRIMAN Db);
        void Save(TRN_PENGIRIMAN Db, Login Login);
    }
}
