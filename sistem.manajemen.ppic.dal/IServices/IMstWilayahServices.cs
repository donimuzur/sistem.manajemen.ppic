using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IMstWilayahServices
    {
        List<MST_WILAYAH> GetAll();
        MST_WILAYAH GetById(object Id);
        void Save(MST_WILAYAH Db);
        void Save(MST_WILAYAH Db, Login Login);
    }
}
