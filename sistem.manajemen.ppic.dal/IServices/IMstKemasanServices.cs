using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IMstKemasanServices
    {
        List<MST_KEMASAN> GetAll();
        MST_KEMASAN GetById(object id);
        void Save(MST_KEMASAN Db);
        void Save(MST_KEMASAN Db, Login Login);
    }
}
