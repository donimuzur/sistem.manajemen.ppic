using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IMstUomServices
    {
        List<MST_UOM> GetAll();
        MST_UOM GetById(object id);
        void Save(MST_UOM Db);
        MST_UOM Save(MST_UOM Db, Login Login);
    }
}
