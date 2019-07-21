using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnDoServices
    {
        List<TRN_DO> GetAll();
        TRN_DO GetById(object Id);
        void Save(TRN_DO Db);
        TRN_DO Save(TRN_DO Db, Login Login);
    }
}
