using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnSpbServices
    {
        List<TRN_SPB> GetActiveAll();
        List<TRN_SPB> GetAll();
        TRN_SPB GetById(object Id);
        void Save(TRN_SPB Db);
        TRN_SPB Save(TRN_SPB Db, Login Login);
        void SaveList(List<TRN_SPB> Db, Login Login);
    }
}
